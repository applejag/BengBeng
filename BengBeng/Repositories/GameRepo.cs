using BengBeng.GameContext.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BengBeng.Repositories
{
    public class GameRepo
    {
        private static List<Game> Games { get; set; }

        public GameRepo()
        {
            Games = new List<Game>();
        }

        public bool SaveGame(Game game)
        {
            if (!Games.Any(x=>x.Id == game.Id))
            {
                Games.Add(game);
                return true;
            }
            return false;
        }

        public Game GetGame(int gameId)
        {
            return Games.SingleOrDefault(x => x.Id == gameId);
        }

        public List<Game> GetGames()
        {
            return Games;
        }
    }
}
