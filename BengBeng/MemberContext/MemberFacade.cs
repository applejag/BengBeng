using System;
using System.Collections.Generic;
using BengBeng.ExternalDependencies;
using BengBeng.Repositories;

namespace BengBeng.MemberContext
{
    public class MemberFacade
    {
        private readonly IFortKnox _billing;

        public MemberFacade(IFortKnox billing)
        {
            _billing = billing;
        }

        public static Member GetMember(string member)
        {
            Console.WriteLine("Fetching member");
            return MemberRepo.GetMember(member);
        }

        public static List<Member> GetAllMembers()
        {
            Console.WriteLine("Fetching members");
            return MemberRepo.GetAllMembers();
        }

        private static Membership GetMembership()
        {
            Console.WriteLine("Adding membership to member");
            return new Membership {ValidYear = DateTime.Now.Year, StartDate = DateTime.Now, IsPayed = true};
        }

        public bool CreateMember(Member member)
        {
            BillMember(member);
            member.Memberships.Add(GetMembership());

            MemberRepo.SaveMember(member);
            return true;
        }

        private bool BillMember(Member member)
        {
            Console.WriteLine("Billing member");
            return _billing.BillMemberFee(member);
        }

        private bool SaveMember(Member member)
        {
            Console.WriteLine("Saving member");
            return MemberRepo.SaveMember(member);
        }
    }
}