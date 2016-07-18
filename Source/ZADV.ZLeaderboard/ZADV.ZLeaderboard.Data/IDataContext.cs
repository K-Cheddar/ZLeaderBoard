using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadv.ZLeaderboard.Domain;
using ZADV.ZLeaderboard.Domain;

namespace ZADV.ZLeaderboard.Data
{
    public interface IDataContext
    {
        DbSet<Event> Events { get; set; }

        DbSet<Participant> Participants { get; set; }

        DbSet<Voter> Voters { get; set; }

        DbSet<T> Set<T>() where T : class, IDbEntity;

        void SaveChanges();
    }
}
