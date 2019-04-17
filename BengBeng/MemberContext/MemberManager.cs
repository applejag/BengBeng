using System;
using System.Collections.Generic;

namespace BengBeng.MemberContext
{
    public class MemberManager
    {
        private readonly MemberFacade _facade;

        public MemberManager(MemberFacade facade)
        {
            _facade = facade;
        }

        public bool CreateMember(Member member)
        {
            Console.WriteLine("Creating new member");
            return _facade.CreateMember(member);
        }

        public Member GetMember(string member)
        {
            return MemberFacade.GetMember(member);
        }

        public List<Member> GetMembers()
        {
            return MemberFacade.GetAllMembers();
        }
    }
}