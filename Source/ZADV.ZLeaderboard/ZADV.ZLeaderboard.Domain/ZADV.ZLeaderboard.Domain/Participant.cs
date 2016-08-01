using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadv.ZLeaderboard.Domain
{
    public class Participant : DbEntity
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public Guid ImageId { get; set; }

        public string Color { get; set; }

        public virtual Event Event { get; set; }
    }
}
