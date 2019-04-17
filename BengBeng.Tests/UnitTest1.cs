using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BengBeng.Adapters;
using BengBeng.ExternalDependencies;
using BengBeng.GameContext;
using BengBeng.GameContext.Factory;
using BengBeng.MemberContext;
using BengBeng.TournamentContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class TestCases
    {
        private static void PlayTournamentGame(List<Member> members, string cupName)
        {
            var _memberAdapter = new MemberAdapter();
            var players = new List<Player>();
            foreach (var member in members)
            {
                players.Add(_memberAdapter.ConvertMemberToPlayer(member));
            }

            var _gameManager = GetGameManager(players);

            _gameManager.PlayTournamentGame(members, cupName);
        }

        [Pure]
        private static TournamentManager GetTournamentManager()
        {
            var mock = new Mock<IFortKnox>();
            mock.Setup(x => x.BillTorunamentFee(It.IsAny<Member>())).Returns(true);
            mock.Setup(x => x.HasMemberPayedTournamentFee(It.IsAny<Member>())).Returns(true);
            var facade = new TournamentFacade(mock.Object);
            return new TournamentManager(facade);
        }

        [Pure]
        private static GameManager GetGameManager(List<Player> players)
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
        private static List<Member> SeedMembers()
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
        private static MemberManager GetMemberManager()
        {
            var mock = new Mock<IFortKnox>();
            mock.Setup(x => x.BillMemberFee(new Member())).Returns(true);
            mock.Setup(x => x.BillTorunamentFee(new Member())).Returns(true);
            mock.Setup(x => x.HasMemberPayedFee(new Member())).Returns(true);
            mock.Setup(x => x.HasMemberPayedTournamentFee(new Member())).Returns(true);

            var facade = new MemberFacade(mock.Object);
            return new MemberManager(facade);
        }

        [TestMethod]
        public void CreateTournament()
        {
            // Arrange
            const string cupName = "Bengans Cup2";
            const string expected = cupName;
            var _tManager = GetTournamentManager();

            // Act
            _tManager.Createtournament(cupName,
                DateTime.Now.AddYears(-1),
                DateTime.Now.AddDays(-60).AddYears(-1)
            );

            // Assert
            var tournament = _tManager.GetTournament(cupName);
            string actual = tournament.Name;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayGame()
        {
            // Arrange
            const string expected = "John Doe";

            var members = SeedMembers().Take(2);
            var adapter = new MemberAdapter();
            var players = members.Select(member => adapter.ConvertMemberToPlayer(member)).ToList();

            var _gameManager = GetGameManager(players);

            // Act
            var game = _gameManager.PlayGame(SeedMembers());

            // Assert
            string actual = game.Winner.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayTournament()
        {
            // Arrange
            const string cupName = "Bengans Cup";
            const string expected = "John Doe";

            var manager = GetTournamentManager();
            var members = SeedMembers();
            manager.Createtournament(cupName, DateTime.Now.AddYears(-1),
                DateTime.Now.AddDays(-60).AddYears(-1));

            foreach (var member in members)
            {
                manager.AddContestant(member, cupName);
            }

            // Act
            for (int i = 0; i < members.Count; i++)
            {
                for (int j = i + 1; j < members.Count; j++)
                {
                    PlayTournamentGame(new List<Member> {members[i], members[j]}, cupName);
                }
            }

            // Assert
            var winner = manager.GetTournamentResult(cupName).Winner;
            string actual = winner.FirstName + " " + winner.Lastname;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayTournamentGameTest()
        {
            // Arrange
            const string cupName = "Bengans Cup3";
            const int expected = 1;

            var adapter = new MemberAdapter();
            var tournamentManager = GetTournamentManager();

            tournamentManager.Createtournament(cupName,
                DateTime.Now.AddYears(-1),
                DateTime.Now.AddDays(-60).AddYears(-1)
            );

            var members = SeedMembers().Take(2).ToList();
            var players = new List<Player>();
            foreach (var member in members)
            {
                players.Add(adapter.ConvertMemberToPlayer(member));
            }

            var gameManager = GetGameManager(players);

            // Act
            gameManager.PlayTournamentGame(members, cupName);

            // Assert
            var tournamentGames = tournamentManager.GetTournament(cupName).Games;
            int actual = tournamentGames.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegisterNewUsers()
        {
            // Arrange
            const int expected = 2;

            var memberManager = GetMemberManager();
            var members = SeedMembers();

            // Act
            memberManager.CreateMember(members[0]);
            memberManager.CreateMember(members[1]);

            // Assert
            int actual = memberManager.GetMembers().Count;

            Assert.AreEqual(expected, actual);
        }
    }
}