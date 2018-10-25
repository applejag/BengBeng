using BengBeng.GameContext.Factory;
using BengBeng.MemberContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace BengBeng.GameContext
{
    public class GameManager
    {
        private readonly GameFacade _facade;

        public GameManager(GameFacade facade)
        {
            _facade = facade;
        }
        public Game PlayGame(List<Member> contestants)
        {
            return _facade.PlayGame(contestants);
        }
        public Game PlayTournamentGame(List<Member> contestants, string tournamentName)
        {
            return _facade.PlayTournamentGame(contestants, tournamentName);
        }
    }
}
