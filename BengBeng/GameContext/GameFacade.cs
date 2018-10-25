using BengBeng.Adapters;
using BengBeng.ExternalDependencies;
using BengBeng.GameContext.Factory;
using BengBeng.MemberContext;
using BengBeng.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BengBeng.GameContext
{
    public class GameFacade
    {
        private readonly ILaneMachine2000 _laneMachine;
        private readonly IFortKnox _billing;
        private readonly GameRepo _gameRepo;
        private readonly MemberAdapter _memberAdapter;
        private readonly GameAdapter _gameAdapter;
        private readonly TournamentRepo _tournamentRepo;

        public GameFacade(ILaneMachine2000 laneMachine, GameRepo gameRepo, IFortKnox billing, TournamentRepo tournamentRepo)
        {
            _laneMachine = laneMachine;
            _gameRepo = gameRepo;
            _billing = billing;
            _memberAdapter = new MemberAdapter();
            _gameAdapter = new GameAdapter();
            _tournamentRepo = tournamentRepo;
        }

        public Game PlayTournamentGame(List<Member> participants, string tournamentName)
        {
            if (MembersEligibleForTournamentGame(participants))
            {
                var contestants = ConvertMembersToPlayerList(participants);
                var machineId = _laneMachine.InitiateGame(contestants);
                var result = GetResult(machineId);
                var game = _gameAdapter.ConvertGameResultToGame(result, tournamentName);
               
                SaveGame(game);
                return game;
            }
            return null;
        }
        public Game PlayGame(List<Member> participants)
        {
            if (MembersEligibleForGame(participants))
            {
                var contestants = ConvertMembersToPlayerList(participants);
                var machineId = _laneMachine.InitiateGame(contestants);
                var result = GetResult(machineId);
                var game = _gameAdapter.ConvertGameResultToGame(result);
                
                SaveGame(game);
                return game;

            }
            return null;
        }

        private GameResult GetResult(int machineId)
        {
            return _laneMachine.GetGameResult(machineId) as GameResult;
        } 

        private bool SaveGame(Game game)
        {
            if (game.IsTournamentGame)
            {
                _tournamentRepo.AddTournamentGame(game);
            }
           return _gameRepo.SaveGame(game);
        }

        private List<Player> ConvertMembersToPlayerList(List<Member> contestants)
        {
            var players = new List<Player>();
            foreach (var member in contestants)
            {
                players.Add(_memberAdapter.ConvertMemberToPlayer(member));
            }
            return players;
        }  

        private bool MembersEligibleForGame(List<Member> contestants)
        {
            
            if (_billing.HasMemberPayedFee(contestants[0]))
            {
                return _billing.HasMemberPayedFee(contestants[1]);
            }
            return false;
        }
        private bool MembersEligibleForTournamentGame(List<Member> contestants)
        {

            if (_billing.HasMemberPayedTournamentFee(contestants[0]))
            {
                return _billing.HasMemberPayedTournamentFee(contestants[1]);
            }
            return false;
        }
    }
}
