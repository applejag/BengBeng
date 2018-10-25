using BengBeng.GameContext.Factory;
using BengBeng.MemberContext;
using BengBeng.TournamentContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BengBeng.Repositories
{
    public static class TournamentRepo
    {
        private static List<Tournament> Tournaments = new List<Tournament>();

        //public static TournamentRepo()
        //{
        //    Tournaments = new List<Tournament>();
        //}

        public static bool AddTournament(Tournament tournament)
        {
            Tournaments.Add(tournament);
            return true;
        }

        public static Tournament GetTournament(string tournamentName)
        {
            return Tournaments.SingleOrDefault(x=>x.Name == tournamentName);
        }

        public static bool AddTournamentGame(Game game)
        {
            Tournaments.SingleOrDefault(x => x.Name == game.TournamentName).Games.Add(game);
            return true;
        }
        public static bool AddTournamentMember(Member member, string tournamentName)
        {
            Tournaments.SingleOrDefault(x => x.Name == tournamentName).Contestants.Add(member);
            return true;
        }

        public static bool UpdateTournament(Tournament tournament)
        {
            Tournaments.Remove(Tournaments.SingleOrDefault(x => x.Name == tournament.Name));
            AddTournament(tournament);
            return true;
        }
        public static Tournament SetTournamentWinner(Member winner, string tournamentName)
        {
            var tournament = Tournaments.SingleOrDefault(x=>x.Name==tournamentName);
            tournament.Winner = winner;
            return tournament;
        }
    }
}
