using BengBeng.ExternalDependencies;
using BengBeng.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BengBeng.MemberContext
{
    public class MemberFacade
    {
        private readonly MemberRepo _memberRepo;
        private readonly IFortKnox _billing;

        public MemberFacade(MemberRepo memberRepo, IFortKnox billing)
        {
            _memberRepo = memberRepo;
            _billing = billing;
        }

        public bool CreateMember(Member member)
        {
            //if (BillMember(member))
            //{
                member.Memberships.Add(NewMembership());
                _memberRepo.SaveMember(member);
                return true;
            //}
            //return false;
        }

        private bool BillMember(Member member)
        {
            return _billing.BillMemberFee(member);
        }

        private Membership NewMembership()
        {
            return new Membership {ValidYear = DateTime.Now.Year, StartDate = DateTime.Now, IsPayed = true};
        }

        private bool SaveMember(Member member)
        {
            return _memberRepo.SaveMember(member);
        }

        public Member GetMember(string member)
        {
            return _memberRepo.GetMember(member);
        }

        public List<Member> GetMembers()
        {
            return _memberRepo.getMembers();
        }
    }
}
