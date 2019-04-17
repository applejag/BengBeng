using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using BengBeng.Adapters;
using BengBeng.ExternalDependencies;
using BengBeng.GameContext;
using BengBeng.MemberContext;
using BengBeng.TournamentContext;
using Moq;

namespace BengBeng.Tests
{
    public static class TestHelper
    {
        public static void PlayTournamentGame(List<Member> members, string cupName)
        {
            var _memberAdapter = new MemberAdapter();
            var players = new List<Player>();
            foreach (var member in members)
            {
                players.Add(_memberAdapter.ConvertMemberToPlayer(member));
            }

            var _gameManager = CreateGameManager(players);

            _gameManager.PlayTournamentGame(members, cupName);
        }

        [Pure]
        public static TournamentManager CreateTournamentManager()
        {
            var mock = new Mock<IFortKnox>();
            mock.Setup(x => x.BillTorunamentFee(It.IsAny<Member>())).Returns(true);
            mock.Setup(x => x.HasMemberPayedTournamentFee(It.IsAny<Member>())).Returns(true);
            var facade = new TournamentFacade(mock.Object);
            return new TournamentManager(facade);
        }

        [Pure]
        public static GameManager CreateGameManager(List<Player> players)
        {
            var billingMock = new Mock<IFortKnox>();
            billingMock.Setup(x => x.BillMemberFee(It.IsAny<Member>())).Returns(true);
            billingMock.Setup(x => x.BillTorunamentFee(It.IsAny<Member>())).Returns(true);
            billingMock.Setup(x => x.HasMemberPayedTournamentFee(It.IsAny<Member>())).Returns(true);
            billingMock.Setup(x => x.HasMemberPayedFee(It.IsAny<Member>())).Returns(true);

            var laneMock = new Mock<ILaneMachine2000>();
            laneMock.Setup(x => x.InitiateGame(It.IsAny<List<Player>>())).Returns(1);

            if (players[0].Name == "John Doe")
            {
                laneMock.Setup(x => x.GetGameResult(It.IsAny<int>())).Returns(new GameResult {
                    MachineId = 1,
                    Contestants = players,
                    Player1Set1Score = 100,
                    Player1Set2Score = 100,
                    Player1Set3Score = 100,
                    Player2Set1Score = 34,
                    Player2Set2Score = 56,
                    Player2Set3Score = 78
                });
            }
            else
            {
                laneMock.Setup(x => x.GetGameResult(It.IsAny<int>())).Returns(new GameResult {
                    MachineId = 1,
                    Contestants = players,
                    Player1Set1Score = 34,
                    Player1Set2Score = 23,
                    Player1Set3Score = 57,
                    Player2Set1Score = 34,
                    Player2Set2Score = 56,
                    Player2Set3Score = 78
                });
            }

            var facade = new GameFacade(laneMock.Object, billingMock.Object);
            return new GameManager(facade);
        }

        [Pure]
        public static List<Member> SeedMembers()
        {
            return new List<Member> {
                new Member {
                    Id = DateTime.Now.ToShortDateString() + 1,
                    FirstName = "Kalle",
                    Lastname = "Jillheden",

                    Adress = "Hemmavägen 42, Sthml, 11665"
                },
                new Member {
                    Id = DateTime.Now.ToShortDateString() + 2,
                    FirstName = "John",
                    Lastname = "Doe",

                    Adress = "Road rd. 10, Sthml, 11635"
                },
                new Member {
                    Id = DateTime.Now.ToShortDateString() + 3,
                    FirstName = "Plopp",
                    Lastname = "Knäck",

                    Adress = "Månen, Sthml, 11635"
                },
                new Member {
                    Id = DateTime.Now.ToShortDateString() + 4,
                    FirstName = "Xeraph",
                    Lastname = "Booswasch",

                    Adress = "Rift, Sthml, 11635"
                },
                new Member {
                    Id = DateTime.Now.ToShortDateString() + 5,
                    FirstName = "Jason",
                    Lastname = "Mickesson",

                    Adress = "Bulleland, Sthml, 11635"
                },
                new Member {
                    Id = DateTime.Now.ToShortDateString() + 6,
                    FirstName = "Olle",
                    Lastname = "Ellosson",

                    Adress = "Sifferholm 10214125, Sthml, 11635"
                },
                new Member {
                    Id = DateTime.Now.ToShortDateString() + 7,
                    FirstName = "Understeck",
                    Lastname = "_________",

                    Adress = "Den gatan runt hörnet, Sthml, 11635"
                },
                new Member {
                    Id = DateTime.Now.ToShortDateString() + 8,
                    FirstName = "Älg",
                    Lastname = "Ren",

                    Adress = "Skogsbrunet ½, Sthml, 11635"
                },
                new Member {
                    Id = DateTime.Now.ToShortDateString() + 9,
                    FirstName = "Tiina",
                    Lastname = "Arvar",

                    Adress = "Meta torget, Sthml, 11635"
                },
                new Member {
                    Id = DateTime.Now.ToShortDateString() + 10,
                    FirstName = "Leena",
                    Lastname = "Büll",

                    Adress = "På tågen, Sthml, 11635"
                },
                new Member {
                    Id = DateTime.Now.ToShortDateString() + 11,
                    FirstName = "Fredrik",
                    Lastname = "Höglund",

                    Adress = "Sonic hedgehocksson, Sthml, 11635"
                }
            };
        }

        [Pure]
        public static MemberManager CreateMemberManager()
        {
            var mock = new Mock<IFortKnox>();
            mock.Setup(x => x.BillMemberFee(new Member())).Returns(true);
            mock.Setup(x => x.BillTorunamentFee(new Member())).Returns(true);
            mock.Setup(x => x.HasMemberPayedFee(new Member())).Returns(true);
            mock.Setup(x => x.HasMemberPayedTournamentFee(new Member())).Returns(true);

            var facade = new MemberFacade(mock.Object);
            return new MemberManager(facade);
        }
    }
}