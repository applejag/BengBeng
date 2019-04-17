using BengBeng.GameContext;
using BengBeng.MemberContext;

namespace BengBeng.Adapters
{
    public class MemberAdapter
    {
        public Player ConvertMemberToPlayer(Member member)
        {
            return new Player
            {
                Id = member.Id,
                Name = member.FirstName + " " + member.Lastname
            };
        }
    }
}