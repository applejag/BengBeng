﻿using System.Collections.Generic;

namespace BengBeng.GameContext.Factory
{
    public class TournamentGame : Game
    {
        public TournamentGame(List<Player> contestants, int machineId, string tournamentName)
        {
            Contestants = new List<Player>();
            TournamentName = tournamentName;
            MachineId = machineId;
            Contestants = contestants;
        }

        public override void ConfigGame()
        {
            IsTournamentGame = true;
        }
    }
}