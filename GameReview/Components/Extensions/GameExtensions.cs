using Components.Models;
using DataAccess.Models;

namespace Components.Extensions
{
    public static class GameExtensions
    {
        public static GameRaw Assign(this GameRaw self, GameDto game)
        {
            self.Id = game.Id;
            self.Name = game.Name;
            return self;
        }

        public static GameDto Assign(this GameDto self, GameRaw game)
        {
            self.Id = game.Id;
            self.Name = game.Name;
            return self;
        }
    }
}
