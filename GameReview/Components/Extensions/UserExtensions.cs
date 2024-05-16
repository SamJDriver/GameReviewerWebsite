using Components.Models;
using DataAccess.Models;

namespace Components.Extensions
{
    public static class UserExtensions
    {
        public static Users Assign(this Users self, UserDto user)
        {
            self.Id = user.Id ?? 0;
            self.Username = user.Username;
            self.PasswordHash = user.PasswordHash;
            self.Salt = user.Salt;
            self.Email = user.Email;
            self.ImageFilePath = user.ImageFilePath;
            self.CreatedBy = user.CreatedBy ?? user.Email;
            self.CreatedDate = user.CreatedDate ?? DateTime.UtcNow;
            self.ModifiedBy = user.ModifiedBy;
            self.ModifiedDate = user.ModifiedDate;
            self.ObsoleteFlag = user.ObsoleteFlag ?? false;
            self.ObsoleteDate = user.ObsoleteDate;
            return self;
        }

        public static UserDto Assign(this UserDto self, Users user)
        {
            self.Id = user.Id;
            self.Username = user.Username;
            self.PasswordHash = user.PasswordHash;
            self.Salt = user.Salt;
            self.Email = user.Email;
            self.ImageFilePath = user.ImageFilePath;
            self.CreatedBy = user.CreatedBy ?? user.Email;
            self.CreatedDate = user.CreatedDate;
            self.ModifiedBy = user.ModifiedBy;
            self.ModifiedDate = user.ModifiedDate;
            self.ObsoleteFlag = user.ObsoleteFlag;
            self.ObsoleteDate = user.ObsoleteDate;
            return self;
        }
    }
}
