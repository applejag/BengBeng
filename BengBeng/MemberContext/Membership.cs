using System;
using System.Collections.Generic;
using System.Text;

namespace BengBeng.MemberContext
{
    public class Membership
    {
        public int ValidYear { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsPayed { get; set; }
    }
}
