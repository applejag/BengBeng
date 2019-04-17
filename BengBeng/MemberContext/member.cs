using System.Collections.Generic;

namespace BengBeng.MemberContext
{
    public class Member
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }

        public List<Membership> Memberships { get; set; }
        public Adress Adress { get; set; }

        public Member()
        {
            Memberships = new List<Membership>();
        }
    }
}