using System;
using System.Collections.Generic;
using BengBeng.ExternalDependencies;
using BengBeng.GameContext;
using BengBeng.MemberContext;

namespace BengBeng
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Create and add two members
            var _memberManager = new MemberManager(new MemberFacade(new FortKnox()));
            var newMember1 = new Member {FirstName = "Kalle", Lastname = "", Adress = new Adress()};
            var newMember2 = new Member {FirstName = "Hobbe", Lastname = "", Adress = new Adress()};
            _memberManager.CreateMember(newMember1);
            _memberManager.CreateMember(newMember2);


            //Retrive them from db and have them play a game
            var _gameManager = new GameManager(new GameFacade(new LaneMachine2000(), new FortKnox()));
            List<Member> members = _memberManager.GetMembers();
            _gameManager.PlayGame(members);
            Console.ReadKey();
        }
    }
}