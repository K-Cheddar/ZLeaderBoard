using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadv.ZLeaderboard.Domain;

namespace Zadv.ZLeaderboard.Domain
{
    public class Voter : DbEntity
    {
        [Required]
        [StringLength(500)]
        public string Email { get; set; }

        public virtual Participant Participant { get; set; }
}
}
