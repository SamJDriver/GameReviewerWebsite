using Components.Models;
using DataAccess.Models.DockerDb;

namespace Components.Extensions
{
    public static class ArtworkExtensions
    {
        public static Artwork Assign(this Artwork self, ArtworkDto artwork)
        {
            self.Id = artwork.Id ?? 0;
            self.GameId = artwork.GameId;
            self.AlphaChannelFlag = artwork.AlphaChannelFlag;
            self.AnimatedFlag = artwork.AnimatedFlag;
            self.Height = artwork.Height;
            self.Width = artwork.Width;
            self.ImageUrl = artwork.ImageUrl;
            return self;
        }

        public static ArtworkDto Assign(this ArtworkDto self, Artwork artwork)
        {
            self.Id = artwork.Id;
            self.GameId = artwork.GameId;
            self.AlphaChannelFlag = artwork.AlphaChannelFlag;
            self.AnimatedFlag = artwork.AnimatedFlag;
            self.Height = artwork.Height;
            self.Width = artwork.Width;
            self.ImageUrl = artwork.ImageUrl;
            return self;
        }
    }
}
