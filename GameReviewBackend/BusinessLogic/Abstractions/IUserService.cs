using System.Runtime.CompilerServices;
using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IUserService
    {
        public int CreateUser(UserDto user);
        public Task<string> LoginEntraId();
    }
}
