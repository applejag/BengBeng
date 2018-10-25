using BengBeng.GameContext.Factory;
using BengBeng.TournamentContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BengBeng.Repositories
{
    public class TournamentRepo
    {
        private static List<Tournament> Tournaments { get; set; }

        public TournamentRepo()
        {
            Tournaments = new List<Tournament>();
        }

        public bool AddTournament(Tournament tournament)
        {
            Tournaments.Add(tournament);
            return true;
        }

        public Tournament GetTournament(string id)
        {
            return Tournaments.SingleOrDefault(x=>x.Name == id);
        }

        public bool AddTournamentGame(Game game)
        {
            Tournaments.SingleOrDefault(x => x.Name == game.TournamentName).Games.Add(game);
            return true;
        }

        public bool UpdateTournament(Tournament tournament)
        {
            Tournaments.Remove(Tournaments.SingleOrDefault(x => x.Name == tournament.Name));
            AddTournament(tournament);
            return true;
        }
    }
}
