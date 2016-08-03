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

        public string Winners { get; set; }

        public string Description { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public UserEventViewModel()
        {
            Participants = new List<ParticipantViewModel>();
        }

    }
}
