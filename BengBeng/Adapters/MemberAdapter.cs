using BengBeng.GameContext;
using BengBeng.MemberContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace BengBeng.Adapters
{
    public class MemberAdapter
    {
        public Player ConvertMemberToPlayer(Member member)
        {
            return new Player {
                Id = member.Id,
                Name = member.FirstName + " " + member.Lastname, 
            };
            
        }
    }
}
