using System.Collections.Generic;
using BengBeng.GameContext;

namespace BengBeng.ExternalDependencies
{
    public interface ILaneMachine2000
    {
        int InitiateGame(List<Player> contestants);
        GameResult GetGameResult(int machineId);
    }
}