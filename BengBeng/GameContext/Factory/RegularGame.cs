using System;
using System.Collections.Generic;
using System.Text;

namespace BengBeng.GameContext.Factory
{
    public class RegularGame : Game
    {
        public RegularGame(List<Player> contestants, int machineId)
        {
            Contestants = new List<Player>();
            Contestants = contestants;
            MachineId = machineId;
            TournamentName = "ordinary game";
        }

        public override void ConfigGame()
        {
            IsTournamentGame = false;
           
        }
    }
}
