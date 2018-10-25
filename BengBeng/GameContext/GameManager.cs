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
            Console.WriteLine("Playing game " + contestants[0].FirstName + " vs " + contestants[1].FirstName);
            return _facade.PlayGame(contestants);
        }
        public Game PlayTournamentGame(List<Member> contestants, string tournamentName)
        {
            Console.WriteLine("Playing tournament game " + contestants[0] + " vs " + contestants[1] + " in " + tournamentName);
            return _facade.PlayTournamentGame(contestants, tournamentName);
        }
    }
}
