using Components.Models;
using DataAccess.Models.DockerDb;

namespace Components.Extensions
{
    public static class UserExtensions
    {
        public static Users Assign(this Users self, UserDto user)
        {
            self.Id = user.Id ?? 0;
            self.Username = user.Username;
            self.PasswordHash = user.Password;
            self.Salt = user.Salt;
            self.Email = user.Email;
            self.ImageFilePath = user.ImageFilePath;
            return self;
        }

        public static UserDto Assign(this UserDto self, Users user)
        {
            self.Id = user.Id;
            self.Username = user.Username;
            self.Password = user.PasswordHash;
            self.Salt = user.Salt;
            self.Email = user.Email;
            self.ImageFilePath = user.ImageFilePath;
            self.CreatedBy = user.CreatedBy;
            self.CreatedDate = user.CreatedDate;
            return self;
        }
    }
}
