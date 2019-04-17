using BengBeng.GameContext;
using BengBeng.GameContext.Factory;

namespace BengBeng.Adapters
{
    public class GameAdapter
    {
        private readonly GameFactory _factory;

        public GameAdapter()
        {
            _factory = new GameFactory();
        }

        public Game ConvertGameResultToGame(GameResult result, string tournamentName = "")
        {
            Game game = _factory.CreateGame(result, tournamentName);
            game.Contestants[0].Score = result.Player1Set1Score + result.Player1Set2Score + result.Player1Set3Score;
            game.Contestants[1].Score = result.Player2Set1Score + result.Player2Set1Score + result.Player2Set3Score;
            game.Winner = game.Contestants[0].Score > game.Contestants[1].Score
                ? game.Contestants[0]
                : game.Contestants[1];
            return game;
        }
    }
}