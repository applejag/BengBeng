using BengBeng.MemberContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BengBeng.Repositories
{
    public class MemberRepo
    {
        private static List<Member> Members { get; set; }
        public MemberRepo()
        {
            Members = new List<Member>();
        }

        public bool SaveMember(Member member)
        {
            if (!Members.Any(x=>x.Id == member.Id))
            {
                Members.Add(member);
                return true;
            }
            return false;
        }

        public Member GetMember(string id)
        {
            return Members.SingleOrDefault(x => x.Id == id);
        }
        public List<Member> getMembers()
        {
            return Members;
        }
    }
}
