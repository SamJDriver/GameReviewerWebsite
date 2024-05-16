using BusinessLogic.Abstractions;
using Components.Extensions;
using Components.Models;
using DataAccess.Contexts;
using DataAccess.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Repositories;
using System.Security.Cryptography;

namespace BusinessLogic.Infrastructure
{
    public class UserService : IUserService
    {
        GenericRepository<NickDbContext> _genericRepository;
        public UserService(GenericRepository<NickDbContext> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public int CreateUser(UserDto user, string? error)
        {
            var existingEmailUser = _genericRepository.GetSingleNoTrack<Users>(u => u.Email == user.Email);
            if ( existingEmailUser != default)
            {
                error = "Email already exists. Please try a different email or log in with your existing email";
                return -1;
            }

            var existingUsernameUser = _genericRepository.GetSingleNoTrack<Users>(u => u.Username == user.Username);
            if (existingUsernameUser != default)
            {
                error = "Username already exists. Please try a different username";
                return -1;
            }

            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            user.PasswordHash = saltPassword(user.PasswordHash, salt);
            user.Salt = Convert.ToBase64String(salt); ;

            Users userEntity = new Users().Assign(user);
            _genericRepository.InsertRecord(userEntity);
            error = default;
            return userEntity.Id;
        }

        private string saltPassword(string password, byte[] salt)
        {
            // TODO i think with how the model is set up right now, we are expecting to receive the salt and hashed password? But that should be something the api will do here
            // derive a 256-bit subkey (use HMACSHA256 with 600,000 iterations)
            // https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 600000,
                numBytesRequested: 256 / 8));

            return hashedPassword;
        }
    }
}
