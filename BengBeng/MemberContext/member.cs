using System;
using System.Collections.Generic;
using System.Text;

namespace BengBeng.MemberContext
{
    public class Member
    {
        public Member()
        {
            Memberships = new List<Membership>();
        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }

        public List<Membership> Memberships { get; set; }
        public Adress Adress { get; set; }
    }
}
