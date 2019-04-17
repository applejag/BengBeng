using System.Collections.Generic;
using System.Linq;
using BengBeng.MemberContext;

namespace BengBeng.Repositories
{
    public static class MemberRepo
    {
        private static readonly List<Member> Members = new List<Member>();

        public static bool SaveMember(Member member)
        {
            Members.Add(member);
            return true;
        }

        public static Member GetMember(string id)
        {
            return Members.SingleOrDefault(x => x.Id == id);
        }

        public static List<Member> GetAllMembers()
        {
            return Members;
        }
    }
}