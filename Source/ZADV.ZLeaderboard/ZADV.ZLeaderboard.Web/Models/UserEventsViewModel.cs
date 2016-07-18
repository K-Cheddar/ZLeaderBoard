using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zadv.ZLeaderboard.Domain;

namespace ZADV.ZLeaderboard.Web.Models
{
    public class UserEventsViewModel
    {
        public List<Event> ActiveEvents { get; set; }
        public List<Event> UpcomingEvents { get; set; }
        public List<Event> PastEvents { get; set; }
    }
}