using System;
using System.Collections.Generic;
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
        private void PlayTournamentGame(List<Member> members, string cupName)
        {
            MemberManager _memberManager = GetMemberManager();

            var _memberAdapter = new MemberAdapter();
            var players = new List<Player>();
            foreach (Member member in members) players.Add(_memberAdapter.ConvertMemberToPlayer(member));

            GameManager _gameManager = GetGameManager(players);

            Game game = _gameManager.PlayTournamentGame(members, cupName);
        }

        private TournamentManager GetTournamentManager()
        {
            var mock = new Mock<IFortKnox>();
            mock.Setup(x => x.BillTorunamentFee(It.IsAny<Member>())).Returns(true);
            mock.Setup(x => x.HasMemberPayedTournamentFee(It.IsAny<Member>())).Returns(true);
            //var repo = new TournamentRepo();
            var facade = new TournamentFacade(mock.Object);
            return new TournamentManager(facade);
        }

        private GameManager GetGameManager(List<Player> players)
        {
            var billingMock = new Mock<IFortKnox>();
            billingMock.Setup(x => x.BillMemberFee(It.IsAny<Member>())).Returns(true);
            billingMock.Setup(x => x.BillTorunamentFee(It.IsAny<Member>())).Returns(true);
            billingMock.Setup(x => x.HasMemberPayedTournamentFee(It.IsAny<Member>())).Returns(true);
            billingMock.Setup(x => x.HasMemberPayedFee(It.IsAny<Member>())).Returns(true);
            var laneMock = new Mock<ILaneMachine2000>();
            laneMock.Setup(x => x.InitiateGame(It.IsAny<List<Player>>())).Returns(1);
            if (players[0].Name == "John Doe")
                laneMock.Setup(x => x.GetGameResult(It.IsAny<int>())).Returns(new GameResult
                {
                    MachineId = 1,
                    Contestants = players,
                    Player1Set1Score = 100,
                    Player1Set2Score = 100,
                    Player1Set3Score = 100,
                    Player2Set1Score = 34,
                    Player2Set2Score = 56,
                    Player2Set3Score = 78
                });
            else
                laneMock.Setup(x => x.GetGameResult(It.IsAny<int>())).Returns(new GameResult
                {
                    MachineId = 1,
                    Contestants = players,
                    Player1Set1Score = 34,
                    Player1Set2Score = 23,
                    Player1Set3Score = 57,
                    Player2Set1Score = 34,
                    Player2Set2Score = 56,
                    Player2Set3Score = 78
                });


            var facade = new GameFacade(laneMock.Object, billingMock.Object);
            return new GameManager(facade);
        }

        private List<Member> SeedMembers()
        {
            var member1 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 1,
                FirstName = "Kalle",
                Lastname = "Jillheden",

                Adress = new Adress
                {
                    Street = "Hemmavägen 42",
                    City = "Sthml",
                    Zip = "11665"
                }
            };
            var member2 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 2,
                FirstName = "John",
                Lastname = "Doe",

                Adress = new Adress
                {
                    Street = "Road rd. 10",
                    City = "Sthml",
                    Zip = "11635"
                }
            };
            var member3 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 3,
                FirstName = "Plopp",
                Lastname = "Knäck",

                Adress = new Adress
                {
                    Street = "Månen",
                    City = "Sthml",
                    Zip = "11635"
                }
            };
            var member4 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 4,
                FirstName = "Xeraph",
                Lastname = "Booswasch",

                Adress = new Adress
                {
                    Street = "Rift",
                    City = "Sthml",
                    Zip = "11635"
                }
            };
            var member5 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 5,
                FirstName = "Jason",
                Lastname = "Mickesson",

                Adress = new Adress
                {
                    Street = "Bulleland",
                    City = "Sthml",
                    Zip = "11635"
                }
            };
            var member6 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 6,
                FirstName = "Olle",
                Lastname = "Ellosson",

                Adress = new Adress
                {
                    Street = "Sifferholm 10214125",
                    City = "Sthml",
                    Zip = "11635"
                }
            };
            var member7 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 7,
                FirstName = "Understeck",
                Lastname = "_________",

                Adress = new Adress
                {
                    Street = "Den gatan runt hörnet",
                    City = "Sthml",
                    Zip = "11635"
                }
            };
            var member8 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 8,
                FirstName = "Älg",
                Lastname = "Ren",

                Adress = new Adress
                {
                    Street = "Skogsbrunet ½",
                    City = "Sthml",
                    Zip = "11635"
                }
            };
            var member9 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 9,
                FirstName = "Tiina",
                Lastname = "Arvar",

                Adress = new Adress
                {
                    Street = "Meta torget",
                    City = "Sthml",
                    Zip = "11635"
                }
            };
            var member10 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 10,
                FirstName = "Leena",
                Lastname = "Büll",

                Adress = new Adress
                {
                    Street = "På tågen",
                    City = "Sthml",
                    Zip = "11635"
                }
            };
            var member11 = new Member
            {
                Id = DateTime.Now.ToShortDateString() + 11,
                FirstName = "Fredrik",
                Lastname = "Höglund",

                Adress = new Adress
                {
                    Street = "Sonic hedgehocksson",
                    City = "Sthml",
                    Zip = "11635"
                }
            };
            return new List<Member>
                {member1, member2, member3, member4, member5, member6, member7, member8, member9, member10, member11};
        }

        private MemberManager GetMemberManager()
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
            TournamentManager _tManager = GetTournamentManager();
            string cupName = "Bengans Cup2";
            _tManager.Createtournament(cupName, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(-60).AddYears(-1));
            Tournament tournament = _tManager.GetTournament(cupName);
            string expected = cupName;
            string actual = tournament.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayGame()
        {
            MemberManager _memberManager = GetMemberManager();
            IEnumerable<Member> members = SeedMembers().Take(2);
            var _memberAdapter = new MemberAdapter();
            var players = new List<Player>();
            foreach (Member member in members) players.Add(_memberAdapter.ConvertMemberToPlayer(member));

            GameManager _gameManager = GetGameManager(players);

            Game game = _gameManager.PlayGame(SeedMembers());
            string expected = "John Doe";
            string actual = game.Winner.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayTournament()
        {
            TournamentManager _tManager = GetTournamentManager();
            var _memberAdapter = new MemberAdapter();
            var players = new List<Player>();
            List<Member> members = SeedMembers();
            string cupName = "Bengans Cup";
            Tournament tournament = _tManager.Createtournament(cupName, DateTime.Now.AddYears(-1),
                DateTime.Now.AddDays(-60).AddYears(-1));

            foreach (Member member in members)
            {
                _tManager.AddContestant(member, cupName);
            }

            for (int i = 0; i < members.Count; i++)
            {
                for (int j = i + 1; j < members.Count; j++)
                {
                    PlayTournamentGame(new List<Member> {members[i], members[j]}, cupName);
                }
            }

            string expected = "John Doe";
            Member winner = _tManager.GetTournamentResult(cupName).Winner;
            string actual = winner.FirstName + " " + winner.Lastname;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayTournamentGameTest()
        {
            var adapter = new MemberAdapter();
            TournamentManager _tManager = GetTournamentManager();

            string cupName = "Bengans Cup3";
            _tManager.Createtournament(cupName, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(-60).AddYears(-1));
            List<Member> members = SeedMembers().Take(2).ToList();
            var players = new List<Player>();
            foreach (Member member in members) players.Add(adapter.ConvertMemberToPlayer(member));
            GameManager _gManager = GetGameManager(players);
            _gManager.PlayTournamentGame(members, cupName);
            List<Game> tournamentGames = _tManager.GetTournament(cupName).Games;
            int expected = 1;
            int actual = tournamentGames.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RegisterNewUsers()
        {
            MemberManager _memberManager = GetMemberManager();
            List<Member> members = SeedMembers();
            _memberManager.CreateMember(members[0]);
            _memberManager.CreateMember(members[1]);

            int expected = 2;
            int actual = _memberManager.GetMembers().Count;

            Assert.AreEqual(expected, actual);
        }
    }
}