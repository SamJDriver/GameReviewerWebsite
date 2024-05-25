using Components.Models;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataAccess.Models.DockerDb;

namespace Components.Extensions
{
    public static class GameExtensions
    {
        public static Games Assign(this Games self, GameDto game)
        {
            self.Id = game.Id ?? 0;
            self.Title = game.Title;
            self.ReleaseDate = game.ReleaseDate;
            self.ImageFilePath = game.ImageFilePath;
            self.Description = game.Description;
            self.CreatedBy = game.CreatedBy;
            self.CreatedDate = game.CreatedDate;
            self.ModifiedBy = game.ModifiedBy;
            self.ModifiedDate = game.ModifiedDate;
            self.ObsoleteFlag = game.ObsoleteFlag;
            self.ObsoleteDate = game.ObsoleteDate;
            return self;
        }

        public static GameDto Assign(this GameDto self, Games game)
        {
            self.Id = game.Id;
            self.Title = game.Title;
            self.ReleaseDate = game.ReleaseDate;
            self.ImageFilePath = game.ImageFilePath;
            self.Description = game.Description;
            self.CreatedBy = game.CreatedBy;
            self.CreatedDate = game.CreatedDate;
            self.ModifiedBy = game.ModifiedBy;
            self.ModifiedDate = game.ModifiedDate;
            self.ObsoleteFlag = game.ObsoleteFlag;
            self.ObsoleteDate = game.ObsoleteDate;
            return self;
        }
    }
}
