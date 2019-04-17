using System;
using System.Collections.Generic;
using BengBeng.Adapters;
using BengBeng.ExternalDependencies;
using BengBeng.GameContext.Factory;
using BengBeng.MemberContext;
using BengBeng.Repositories;

namespace BengBeng.GameContext
{
    public class GameFacade
    {
        private readonly IFortKnox _billing;
        private readonly GameAdapter _gameAdapter;
        private readonly ILaneMachine2000 _laneMachine;
        private readonly MemberAdapter _memberAdapter;

        public GameFacade(ILaneMachine2000 laneMachine, IFortKnox billing)
        {
            _laneMachine = laneMachine;
            _billing = billing;
            _memberAdapter = new MemberAdapter();
            _gameAdapter = new GameAdapter();
        }

        public Game PlayTournamentGame(List<Member> participants, string tournamentName)
        {
            if (MembersEligibleForTournamentGame(participants))
            {
                List<Player> contestants = ConvertMembersToPlayerList(participants);
                int machineId = _laneMachine.InitiateGame(contestants);
                GameResult result = GetResult(machineId);
                Game game = _gameAdapter.ConvertGameResultToGame(result, tournamentName);
                game.ConfigGame();

                SaveGame(game);
                return game;
            }

            return null;
        }

        public Game PlayGame(List<Member> participants)
        {
            if (MembersEligibleForGame(participants))
            {
                List<Player> contestants = ConvertMembersToPlayerList(participants);
                int machineId = _laneMachine.InitiateGame(contestants);
                Console.WriteLine("Playing game");
                GameResult result = GetResult(machineId);
                Console.WriteLine("Game results:");

                Game game = _gameAdapter.ConvertGameResultToGame(result);
                Console.WriteLine(game.Winner.Name + " wins with " + game.Winner.Score + " points");
                game.ConfigGame();
                SaveGame(game);
                return game;
            }

            return null;
        }

        private GameResult GetResult(int machineId)
        {
            Console.WriteLine("Getting results from lane system...");
            return _laneMachine.GetGameResult(machineId);
        }

        private bool SaveGame(Game game)
        {
            if (game.IsTournamentGame) TournamentRepo.AddTournamentGame(game);
            Console.WriteLine("Saving game to database");
            return GameRepo.SaveGame(game);
        }

        private List<Player> ConvertMembersToPlayerList(List<Member> contestants)
        {
            var players = new List<Player>();
            foreach (Member member in contestants) players.Add(_memberAdapter.ConvertMemberToPlayer(member));
            return players;
        }

        private bool MembersEligibleForGame(List<Member> contestants)
        {
            Console.WriteLine("Checking contestants billed status...");
            if (_billing.HasMemberPayedFee(contestants[0]))
            {
                Console.WriteLine("Contestants passed");
                return _billing.HasMemberPayedFee(contestants[1]);
            }

            return false;
        }

        private bool MembersEligibleForTournamentGame(List<Member> contestants)
        {
            Console.WriteLine("Checking contestants billed status...");
            if (_billing.HasMemberPayedTournamentFee(contestants[0]))
            {
                Console.WriteLine("Contestants passed");
                return _billing.HasMemberPayedTournamentFee(contestants[1]);
            }

            return false;
        }
    }
}