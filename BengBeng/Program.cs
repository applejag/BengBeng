using BengBeng.ExternalDependencies;
using BengBeng.GameContext;
using BengBeng.MemberContext;
using System;
using System.Collections.Generic;

namespace BengBeng
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create and add two members
            MemberManager _memberManager = new MemberManager(new MemberFacade(new FortKnox()));
            var newMember1 = new Member { FirstName = "Alexander", Lastname = "", Adress = new Adress { } };
            var newMember2 = new Member { FirstName = "Gustav", Lastname = "", Adress = new Adress { } };
            _memberManager.CreateMember(newMember1);
            _memberManager.CreateMember(newMember2);
            

            //Retrive them from db and have them play a game
            GameManager _gameManager = new GameManager(new GameFacade(new LaneMachine2000(), new FortKnox()));
            var members = _memberManager.GetMembers();
            _gameManager.PlayGame(members);
            Console.ReadKey();
        }
    }
}
