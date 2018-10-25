using BengBeng.ExternalDependencies;
using BengBeng.GameContext;
using BengBeng.MemberContext;
using BengBeng.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BengBeng.TournamentContext
{
    public class TournamentFacade
    {
        //private readonly TournamentRepo _repo;
        private readonly IFortKnox _billing;

        public TournamentFacade(IFortKnox billing)
        {
            //_repo = repo;
            
            _billing = billing;
        }

        public Tournament CreateTournament(string name, DateTime to, DateTime from)
        {
            var tournament = new Tournament { Name = name, From = from, To = to };
            TournamentRepo.AddTournament(tournament);
            return tournament;
        }

        public bool AddContestant(Member member, string tournamentName)
        {
            return TournamentRepo.AddTournamentMember(member, tournamentName);
        }
        private bool BillTorunamentFee(Member member)
        {
            return _billing.BillTorunamentFee(member);
        }
       
        private bool UpdateTournament(Tournament updatedTournament)
        {
            return TournamentRepo.UpdateTournament(updatedTournament);
        }

        public Tournament GetTournamentResult(string tournamentName)
        {
           
            var winner = GetTournamentWinner(tournamentName);
            return TournamentRepo.SetTournamentWinner(winner, tournamentName);
        }

        
        private Member GetTournamentWinner(string tournamentName)
        {
            var tournament = TournamentRepo.GetTournament(tournamentName);
            //var winnerPlayer = tournament.Games.First().Winner;
            //var winnerPlayer = tournament.Games.GroupBy(s => s.Winner)
            //             .OrderByDescending(s => s.Count())
            //             .First().Key;
            var winnerPlayer = tournament.Games
            .GroupBy(x => x.Winner)
            .Select(x => new
            {
                Winner = x.Key,
                Count = x.Count()
            })
            .OrderByDescending(x => x.Count)
            .Select(x => x.Winner)
            .First();

            var winner = MemberRepo.GetMember(winnerPlayer.Id);
            return winner;
        }

        public Tournament GetTournament(string tournamentName)
        {
            return TournamentRepo.GetTournament(tournamentName);
        }
    }
}
