using System;
using System.Collections.Generic;
using BengBeng.GameContext.Factory;
using BengBeng.MemberContext;

namespace BengBeng.TournamentContext
{
    public class Tournament
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<Game> Games { get; set; }
        public List<Member> Contestants { get; set; }
        public Member Winner { get; set; }

        public Tournament()
        {
            Contestants = new List<Member>();
            Games = new List<Game>();
        }
    }
}