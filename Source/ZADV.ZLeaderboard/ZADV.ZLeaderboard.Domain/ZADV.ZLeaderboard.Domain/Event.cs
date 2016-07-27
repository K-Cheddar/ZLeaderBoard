using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadv.ZLeaderboard.Domain
{
    public class Event : DbEntity
    {
        public Event()
        {
            IsActive = true;
        }

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
    }
}
