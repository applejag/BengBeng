using System;
using System.Collections.Generic;
using System.Text;

namespace BengBeng.GameContext.Factory
{
    public abstract class Game
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public List<Player> Contestants { get; set; }
        public Player Winner { get; set; }
        public bool IsTournamentGame { get; set; }
        public string TournamentName { get; set; }

        public Game()
        {
            Contestants = new List<Player>();

        }

        public abstract void ConfigGame();
    }
}
