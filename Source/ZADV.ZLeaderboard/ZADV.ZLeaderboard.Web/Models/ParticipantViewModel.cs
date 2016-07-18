﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZADV.ZLeaderboard.Web.Models
{
    public class ParticipantViewModel
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int Id { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }


    }
}