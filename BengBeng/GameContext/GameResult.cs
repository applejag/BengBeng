using System;
using System.Collections.Generic;
using System.Text;

namespace BengBeng.GameContext
{
    public class GameResult
    {
        public GameResult()
        {
            Contestants = new List<Player>();
        }
        public int MachineId { get; set; }
        public List<Player> Contestants { get; set; }
        public double Player1Set1Score { get; set; }
        public double Player1Set2Score { get; set; }
        public double Player1Set3Score { get; set; }
        public double Player2Set1Score { get; set; }
        public double Player2Set2Score { get; set; }
        public double Player2Set3Score { get; set; }
    }
}
