using System;
using System.Collections.Generic;
using System.Text;

namespace BengBeng.GameContext.Factory
{
    public class GameFactory
    {
        public Game CreateGame(GameResult result, string tournamentName = "")
        {
            if (tournamentName != "")
            {
                return new TournamentGame(result.Contestants, result.MachineId, tournamentName);
            }
            else
            {
                return new RegularGame(result.Contestants, result.MachineId);
            }
        }
    }
}
