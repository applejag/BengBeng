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

        public bool CreateMember(Member member)
        {
            BillMember(member);
            member.Memberships.Add(NewMembership());

            MemberRepo.SaveMember(member);
            return true;
        }

        public Member GetMember(string member)
        {
            Console.WriteLine("Fetching member");
            return MemberRepo.GetMember(member);
        }

        public List<Member> GetMembers()
        {
            Console.WriteLine("Fetching members");
            return MemberRepo.GetMembers();
        }

        private bool BillMember(Member member)
        {
            Console.WriteLine("Billing member");
            return _billing.BillMemberFee(member);
        }

        private Membership NewMembership()
        {
            Console.WriteLine("Adding membership to member");
            return new Membership {ValidYear = DateTime.Now.Year, StartDate = DateTime.Now, IsPayed = true};
        }

        private bool SaveMember(Member member)
        {
            Console.WriteLine("Saving member");
            return MemberRepo.SaveMember(member);
        }
    }
}