using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZADV.ZLeaderboard.Web.Models
{
    public class EventViewModel
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        public DateTime StartAt { get; set; }

        [Required]
        public DateTime EndAt { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string Description { get; set; }

        public bool MultipleVotes { get; set; }

        public bool ResetVotes { get; set; }

        public List<ParticipantViewModel> Participants { get; set; }

        public EventViewModel()
        {
            Participants = new List<ParticipantViewModel>();
        }
    }

}