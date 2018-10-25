using BengBeng.GameContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace BengBeng.ExternalDependencies
{
    public interface ILaneMachine2000
    {
        int InitiateGame(List<Player> contestants);
        GameResult GetGameResult(int machineId);
    }
}
