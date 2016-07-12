using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Zadv.ZLeaderboard.Domain.IRepositories
{
    public interface IBaseRepository<T> where T : class, IDbEntity
    {
        void Add(T dbEntity);

        void Remove(T dbEntity);

        void Update(T dbEntity);

        T Get(int id);

        T GetWithReferences(int id, params Expression<Func<T, object>>[] includes);

        IList<T> GetAll();

        T FirstOrDefault();
    }
}
