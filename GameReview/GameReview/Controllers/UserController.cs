using BusinessLogic.Infrastructure;
using BusinessLogic.Models;
using DataAccess.Contexts;
using DataAccess.Models;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;



namespace GameReview.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private NickDbContext _context;

        public UserController(NickDbContext _context)
        {
            
        }

        [AllowAnonymous]
        [HttpPost("/createUser")]
        public IActionResult CreateUser([FromBody] UserDto userModel)
        {

            // TODO make sure these validations make sense
            // validate
            if (_context.Users.Any(x => x.Username == userModel.Username))
                return BadRequest($"{userModel.Username} is already taken. Please try a different username");

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided.");
            }

            // TODO what is this mapper
            // map model to new user object
            var user = _mapper.Map<User>(userModel);

            // https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-8.0
            // Generate a 128-bit salt using a sequence of
            // cryptographically strong random bytes.
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes

            // TODO i think with how the model is set up right now, we are expecting to receive the salt and hashed password? But that should be something the api will do here
            // derive a 256-bit subkey (use HMACSHA256 with 600,000 iterations)
            // https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userModel.password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 600000,
                numBytesRequested: 256 / 8));

            //TODO how to save it
            // save user
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok();
        }


        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok();
        }


    }
}