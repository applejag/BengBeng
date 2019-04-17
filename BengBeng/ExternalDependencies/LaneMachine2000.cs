using System.Collections.Generic;
using BengBeng.GameContext;

namespace BengBeng.ExternalDependencies
{
    public class LaneMachine2000 : ILaneMachine2000
    {
        public GameResult GetGameResult(int machineId)
        {
            return new GameResult
            {
                MachineId = 1,
                Contestants = new List<Player> {new Player {Name = "Alexander"}, new Player {Name = "Gustav"}},
                Player1Set1Score = 34,
                Player1Set2Score = 23,
                Player1Set3Score = 57,
                Player2Set1Score = 34,
                Player2Set2Score = 56,
                Player2Set3Score = 78
            };
        }

        public int InitiateGame(List<Player> contestants)
        {
            return 1;
        }
    }
}