using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZADV.ZLeaderboard.Web.Models
{
    public class EmailInputViewModel
    {
        [Required]
        [StringLength(500)]
        public string Email { get; set; }
    }
}