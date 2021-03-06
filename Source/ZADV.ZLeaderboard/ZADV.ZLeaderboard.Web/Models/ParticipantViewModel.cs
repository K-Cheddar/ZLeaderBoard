﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;
using Zadv.ZLeaderboard.Domain;

namespace ZADV.ZLeaderboard.Web.Models
{
    public class ParticipantViewModel
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int Id { get; set; }

        public int? VoteCount { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public String Color { get; set; }

        public bool VotedFor { get; set; }
    }
}