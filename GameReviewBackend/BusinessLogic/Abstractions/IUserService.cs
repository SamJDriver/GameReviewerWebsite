using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IUserService
    {
        public int CreateUser(UserDto user);
    }
}
