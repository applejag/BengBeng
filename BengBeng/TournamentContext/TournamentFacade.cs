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
        private readonly TournamentRepo _repo;
        private readonly IFortKnox _billing;

        public TournamentFacade(TournamentRepo repo, IFortKnox billing)
        {
            _repo = repo;
            _billing = billing;
        }

        public Tournament CreateTournament(string name, DateTime to, DateTime from)
        {
            var tournament = new Tournament { Name = name, From = from, To = to };
            _repo.AddTournament(tournament);
            return tournament;
        }

        public bool AddContestant(Member member, string tournamentName)
        {
            //if (BillTorunamentFee(member))
            //{
                var tournament = AddToTournament(member, tournamentName);
                UpdateTournament(tournament);
                return true;
            //}
            //return false;
        }
        private bool BillTorunamentFee(Member member)
        {
            return _billing.BillTorunamentFee(member);
        }
        private Tournament AddToTournament(Member member, string id)
        {
            var tournament = _repo.GetTournament(id);
            tournament.Contestants.Add(member);
            return tournament;
        }
        private bool UpdateTournament(Tournament updatedTournament)
        {
            return _repo.UpdateTournament(updatedTournament);
        }

        public Tournament GetTournamentResult(string tournamentName)
        {
            var tournament = _repo.GetTournament(tournamentName);
            return SetTournamentWinner(tournament);
        }

        private Tournament SetTournamentWinner(Tournament tournament)
        {
          
            var winner = tournament.Games.GroupBy(s => s.Winner)
                         .OrderByDescending(s => s.Count())
                         .First().Key;

            tournament.Winner = tournament.Contestants.SingleOrDefault(x => x.FirstName + " " + x.Lastname == winner.Name);
            return tournament;
        }
    }
}
