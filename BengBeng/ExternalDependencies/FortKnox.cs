using System;
using System.Collections.Generic;
using System.Text;
using BengBeng.MemberContext;

namespace BengBeng.ExternalDependencies
{
    public class FortKnox : IFortKnox
    {
        public bool BillMemberFee(Member member)
        {
            return true;
        }

        public void BillMemberFee(Member member1, object member2)
        {
            throw new NotImplementedException();
        }

        public bool BillTorunamentFee(Member member)
        {
            return true;
        }

        public bool HasMemberPayedFee(Member member)
        {
            return true;
        }

        public bool HasMemberPayedTournamentFee(Member member)
        {
            return true;
        }
    }
}
