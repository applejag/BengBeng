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

            return new RegularGame(result.Contestants, result.MachineId);
        }
    }
}