using BengBeng.Adapters;
using BengBeng.ExternalDependencies;
using BengBeng.GameContext;
using BengBeng.MemberContext;
using BengBeng.Repositories;
using BengBeng.TournamentContext;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class TestCases
    {
        [Fact]
        public void RegisterNewUsers()
        {
            var _memberManager = GetMemberManager();
            var members = SeedMembers();
            _memberManager.CreateMember(members[0]);
            _memberManager.CreateMember(members[1]);

            var expected = 11;
            var actual = _memberManager.GetMembers().Count;

            Assert.Equal(expected, actual);

        }
        [Fact]
        public void PlayGame()
        {
            var _memberManager = GetMemberManager();
            var members = SeedMembers().Take(2);
            var _memberAdapter = new MemberAdapter();
            var players = new List<Player>();
            foreach (var member in members)
            {
                players.Add(_memberAdapter.ConvertMemberToPlayer(member));
            }
           
            var _gameManager = GetGameManager(players);

            var game = _gameManager.PlayGame(SeedMembers());
            var expected = "Alex Arvanitis";
            var actual = game.Winner.Name;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PlayTournament()
        {
            var _tManager = GetTournamentManager();
            var _memberAdapter = new MemberAdapter();
            var players = new List<Player>();
            var members = SeedMembers();
            var cupName = "Bengans Cup";
            var tournament = _tManager.Createtournament(cupName, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(-60).AddYears(-1) );

            foreach (var member in members)
            {
                _tManager.AddContestant(member, cupName);
            }
            for (int i = 0; i < members.Count; i++)
            {
                for (int j = i+1; j < members.Count; j++)
                {
                    PlayTournamentGame(new List<Member> { members[i], members[j] }, cupName);
                }
            }
            var expected = "Alex Arvanitis";
            var winner = _tManager.GetTournamentResult(cupName).Winner;
            var actual = winner.FirstName + " " + winner.Lastname;

            Assert.Equal(expected, actual);
        }

        private void PlayTournamentGame(List<Member> members, string cupName)
        {
            var _memberManager = GetMemberManager();
            
            var _memberAdapter = new MemberAdapter();
            var players = new List<Player>();
            foreach (var member in members)
            {
                players.Add(_memberAdapter.ConvertMemberToPlayer(member));
            }

            var _gameManager = GetGameManager(players);

            var game = _gameManager.PlayTournamentGame(members, cupName);
        }

        private TournamentManager GetTournamentManager()
        {
            var mock = new Mock<IFortKnox>();
            mock.Setup(x => x.BillTorunamentFee(It.IsAny<Member>())).Returns(true);
            mock.Setup(x => x.HasMemberPayedTournamentFee(It.IsAny<Member>())).Returns(true);
            var repo = new TournamentRepo();
            var facade = new TournamentFacade(repo, mock.Object);
            return new TournamentManager(facade);
        }

        private GameManager GetGameManager(List<Player> players)
        {
            var billingMock = new Mock<IFortKnox>();
            billingMock.Setup(x=>x.BillMemberFee(It.IsAny<Member>())).Returns(true);
            billingMock.Setup(x => x.BillTorunamentFee(It.IsAny<Member>())).Returns(true);
            billingMock.Setup(x => x.HasMemberPayedTournamentFee(It.IsAny<Member>())).Returns(true);
            billingMock.Setup(x => x.HasMemberPayedFee(It.IsAny<Member>())).Returns(true);
            var laneMock = new Mock<ILaneMachine2000>();
            laneMock.Setup(x => x.InitiateGame(It.IsAny<List<Player>>())).Returns(1);
            if (players[0].Name == "Alex Arvanitis")
            {
                laneMock.Setup(x => x.GetGameResult(It.IsAny<int>())).Returns(new GameResult
                {
                    MachineId = 1,
                    Contestants = players,
                    Player1Set1Score = 100,
                    Player1Set2Score = 100,
                    Player1Set3Score = 100,
                    Player2Set1Score = 34,
                    Player2Set2Score = 56,
                    Player2Set3Score = 78,
                });
            }
            else
            {
                laneMock.Setup(x => x.GetGameResult(It.IsAny<int>())).Returns(new GameResult
                {
                    MachineId = 1,
                    Contestants = players,
                    Player1Set1Score = 34,
                    Player1Set2Score = 23,
                    Player1Set3Score = 57,
                    Player2Set1Score = 34,
                    Player2Set2Score = 56,
                    Player2Set3Score = 78,
                });
            }
           
            var repo = new GameRepo();
            var tRepo = new TournamentRepo();
            var facade = new GameFacade(laneMock.Object, repo, billingMock.Object, tRepo);
            return new GameManager(facade);
        }

        private List<Member> SeedMembers()
        {
            var member1 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 1,
                FirstName = "Alex",
                Lastname = "Arvanitis",

                Adress = new Adress
                {
                    Street = "Gotlandsgatan 50",
                    City = "Sthml",
                    Zip = "11665"
                },

            };
            var member2 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 2,
                FirstName = "Gustav",
                Lastname = "Cleveman",

                Adress = new Adress
                {
                    Street = "Metargatan 10",
                    City = "Sthml",
                    Zip = "11635"
                },

            };
            var member3 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 3,
                FirstName = "Gustav",
                Lastname = "Arvanitis",

                Adress = new Adress
                {
                    Street = "Metargatan 10",
                    City = "Sthml",
                    Zip = "11635"
                },

            };
            var member4 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 4,
                FirstName = "Emilie",
                Lastname = "Cleveman",

                Adress = new Adress
                {
                    Street = "Metargatan 10",
                    City = "Sthml",
                    Zip = "11635"
                },

            };
            var member5 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 5,
                FirstName = "Jason",
                Lastname = "Arvanitis",

                Adress = new Adress
                {
                    Street = "Metargatan 10",
                    City = "Sthml",
                    Zip = "11635"
                },

            };
            var member6 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 6,
                FirstName = "Julia",
                Lastname = "Arvanitis",

                Adress = new Adress
                {
                    Street = "Metargatan 10",
                    City = "Sthml",
                    Zip = "11635"
                },

            };
            var member7 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 7,
                FirstName = "Emilie",
                Lastname = "Arvanitis",

                Adress = new Adress
                {
                    Street = "Metargatan 10",
                    City = "Sthml",
                    Zip = "11635"
                },

            };
            var member8 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 8,
                FirstName = "Emilie",
                Lastname = "Glans",

                Adress = new Adress
                {
                    Street = "Metargatan 10",
                    City = "Sthml",
                    Zip = "11635"
                },

            };
            var member9 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 9,
                FirstName = "Tiina",
                Lastname = "Arvanitis",

                Adress = new Adress
                {
                    Street = "Metargatan 10",
                    City = "Sthml",
                    Zip = "11635"
                },

            };
            var member10 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 10,
                FirstName = "Leena",
                Lastname = "Arvanitis",

                Adress = new Adress
                {
                    Street = "Metargatan 10",
                    City = "Sthml",
                    Zip = "11635"
                },

            };
            var member11 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 11,
                FirstName = "Karim",
                Lastname = "Hamza",

                Adress = new Adress
                {
                    Street = "Metargatan 10",
                    City = "Sthml",
                    Zip = "11635"
                },

            };
            return new List<Member> { member1, member2, member3, member4, member5, member6, member7, member8, member9, member10, member11 };
        }

        private MemberManager GetMemberManager()
        {
            var mock = new Mock<IFortKnox>();
            mock.Setup(x => x.BillMemberFee(new Member())).Returns(true);
            mock.Setup(x => x.BillTorunamentFee(new Member())).Returns(true);
            mock.Setup(x => x.HasMemberPayedFee(new Member())).Returns(true);
            mock.Setup(x => x.HasMemberPayedTournamentFee(new Member())).Returns(true);
            var repo = new MemberRepo();
            var facade = new MemberFacade(repo,mock.Object);
            return new MemberManager(facade);
        }
    }
}
