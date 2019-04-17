using BengBeng.MemberContext;

namespace BengBeng.ExternalDependencies
{
    public interface IFortKnox
    {
        bool BillMemberFee(Member member);
        bool HasMemberPayedFee(Member member);
        bool BillTorunamentFee(Member member);
        bool HasMemberPayedTournamentFee(Member member);
        void BillMemberFee(Member member1, object member2);
    }
}