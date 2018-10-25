using BengBeng.GameContext.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BengBeng.Repositories
{
    public static class GameRepo
    {
        private static List<Game> Games = new List<Game>();

        

        public static bool SaveGame(Game game)
        {
            if (!Games.Any(x=>x.Id == game.Id))
            {
                Games.Add(game);
                return true;
            }
            return false;
        }

        public static Game GetGame(int gameId)
        {
            return Games.SingleOrDefault(x => x.Id == gameId);
        }

        public static List<Game> GetGames()
        {
            return Games;
        }
    }
}
