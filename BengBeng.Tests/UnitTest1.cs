using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BengBeng.Adapters;
using BengBeng.ExternalDependencies;
using BengBeng.GameContext;
using BengBeng.GameContext.Factory;
using BengBeng.MemberContext;
using BengBeng.Tests;
using BengBeng.TournamentContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class TestCases
    {

        [TestMethod]
        public void CreateTournament()
        {
            // Arrange
            const string cupName = "Bengans Cup2";
            const string expected = cupName;
            var _tManager = TestHelper.CreateTournamentManager();

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

            var members = TestHelper.SeedMembers().Take(2);
            var adapter = new MemberAdapter();
            var players = members.Select(member => adapter.ConvertMemberToPlayer(member)).ToList();

            var _gameManager = TestHelper.CreateGameManager(players);

            // Act
            var game = _gameManager.PlayGame(TestHelper.SeedMembers());

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

            var manager = TestHelper.CreateTournamentManager();
            var members = TestHelper.SeedMembers();
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
                    TestHelper.PlayTournamentGame(new List<Member> {members[i], members[j]}, cupName);
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
            var tournamentManager = TestHelper.CreateTournamentManager();

            tournamentManager.Createtournament(cupName,
                DateTime.Now.AddYears(-1),
                DateTime.Now.AddDays(-60).AddYears(-1)
            );

            var members = TestHelper.SeedMembers().Take(2).ToList();
            var players = new List<Player>();
            foreach (var member in members)
            {
                players.Add(adapter.ConvertMemberToPlayer(member));
            }

            var gameManager = TestHelper.CreateGameManager(players);

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

            var memberManager = TestHelper.CreateMemberManager();
            var members = TestHelper.SeedMembers();

            // Act
            memberManager.CreateMember(members[0]);
            memberManager.CreateMember(members[1]);

            // Assert
            int actual = memberManager.GetMembers().Count;

            Assert.AreEqual(expected, actual);
        }
    }
}