using BusinessLogic.Abstractions;
using Components.Exceptions;
using Components.Extensions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Repositories;
using System.Diagnostics;
using System.Security.Cryptography;

namespace BusinessLogic.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly GenericRepository<DockerDbContext> _genericRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public UserService(GenericRepository<DockerDbContext> genericRepository, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _genericRepository = genericRepository;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public int CreateUser(UserDto user)
        {
            //var existingEmailUser = _genericRepository.GetSingleNoTrack<Users>(u => u.Email == user.Email);
            //if ( existingEmailUser != default)
            //{
            //    throw new DgcException("Error creating user. An account with that email already exists.", DgcExceptionType.InvalidOperation);
            //}

            //var existingUsernameUser = _genericRepository.GetSingleNoTrack<Users>(u => u.Username == user.Username);
            //if (existingUsernameUser != default)
            //{
            //    throw new DgcException("Error creating user. An account with that username already exists.", DgcExceptionType.InvalidOperation);
            //}

            //byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            //user.Password = saltPassword(user.Password, salt);
            //user.Salt = Convert.ToBase64String(salt); ;

            //Users userEntity = new Users().Assign(user);
            //DockerDbContext.SetUsername(user.Username);
            //_genericRepository.InsertRecord(userEntity);
            //return userEntity.Id;

            return 0;
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
