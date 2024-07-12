using Components.Models;
using DataAccess.Models.DockerDb;

namespace Components.Extensions
{
    public static class GameExtensions
    {
        public static Games Assign(this Games self, GameDto game)
        {
            self.Title = game.Title;
            self.ReleaseDate = game.ReleaseDate;
            self.Description = game.Description;
            self.Artwork = game.Artwork != null ? game.Artwork.Select(a => new Artwork().Assign(a)).ToList() : null;
            self.Cover = game.Cover != null ? game.Cover.Select(c => new Cover().Assign(c)).ToList() : null;
            return self;
        }

        public static GameDto Assign(this GameDto self, Games game)
        {
            self.Id = game.Id;
            self.Title = game.Title;
            self.ReleaseDate = game.ReleaseDate;
            self.Description = game.Description;
            self.Artwork = game.Artwork != null ? game.Artwork.Select(a => new ArtworkDto().Assign(a)).ToList() : null;
            self.Cover = game.Cover != null ? game.Cover.Select(a => new CoverDto().Assign(a)).ToList() : null;
            // self.ParentLinks = game.GameSelfLinkParentGame != null ? game.GameSelfLinkParentGame.Select(g => new GameSelfLinkDto().Assign(g)).ToList() : null;
            // self.ChildLinks = game.GameSelfLinkChildGame != null ? game.GameSelfLinkChildGame.Select(g => new GameSelfLinkDto().Assign(g)).ToList() : null;
            return self;
        }
    }
}
