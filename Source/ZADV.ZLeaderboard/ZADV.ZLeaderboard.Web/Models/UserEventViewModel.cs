using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zadv.ZLeaderboard.Domain;

namespace ZADV.ZLeaderboard.Web.Models
{
    public class UserEventViewModel
    {

        public string Name { get; set; }

        public List<ParticipantViewModel> Participants { get; set; }

        public UserEventViewModel()
        {
            Participants = new List<ParticipantViewModel>();
        }

    }
}
