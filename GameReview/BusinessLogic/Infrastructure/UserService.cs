using BusinessLogic.Abstractions;
using BusinessLogic.Models;
using DataAccess;
using DataAccess.Contexts;
using DataAccess.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Infrastructure
{
    public class UserService : IUserService
    {
        GenericRepository<NickDbContext> _genericRepository;
        public UserService(GenericRepository<NickDbContext> genericRepository)
        {
            _genericRepository = genericRepository;
        }


        //public void CreateUser(UserDto user)
        //{
        //    Users userEntity = new Users().Assign();
        //    _genericRepository.InsertRecord
        //}
    }
}
