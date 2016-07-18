using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadv.ZLeaderboard.Domain;
using Zadv.ZLeaderboard.Domain.IRepositories;
using ZADV.ZLeaderboard.Domain;
using ZADV.ZLeaderboard.Domain.IRepositories;

namespace ZADV.ZLeaderboard.Data
{
    public class VoterRepository : BaseRepository<Voter>, IVoterRepository
    {
    }
}
