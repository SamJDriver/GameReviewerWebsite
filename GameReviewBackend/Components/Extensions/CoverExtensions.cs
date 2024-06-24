using Components.Models;
using DataAccess.Models.DockerDb;

namespace Components.Extensions
{
    public static class CoverExtensions
    {
        public static Cover Assign(this Cover self, CoverDto Cover)
        {
            self.Id = Cover.Id ?? 0;
            self.GameId = Cover.GameId;
            self.AlphaChannelFlag = Cover.AlphaChannelFlag;
            self.AnimatedFlag = Cover.AnimatedFlag;
            self.Height = Cover.Height;
            self.Width = Cover.Width;
            self.ImageUrl = Cover.ImageUrl;
            self.CreatedBy = Cover.CreatedBy;
            self.CreatedDate = Cover.CreatedDate;
            return self;
        }

        public static CoverDto Assign(this CoverDto self, Cover Cover)
        {
            self.Id = Cover.Id;
            self.GameId = Cover.GameId;
            self.AlphaChannelFlag = Cover.AlphaChannelFlag;
            self.AnimatedFlag = Cover.AnimatedFlag;
            self.Height = Cover.Height;
            self.Width = Cover.Width;
            self.ImageUrl = Cover.ImageUrl;
            self.CreatedBy = Cover.CreatedBy;
            self.CreatedDate = Cover.CreatedDate;
            return self;
        }
    }
}
