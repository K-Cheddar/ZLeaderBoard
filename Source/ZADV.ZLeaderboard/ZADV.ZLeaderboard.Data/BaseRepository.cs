using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zadv.ZLeaderboard.Domain;
using Zadv.ZLeaderboard.Domain.IRepositories;

namespace ZADV.ZLeaderboard.Data
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IDbEntity
    {
        [Dependency]
        public virtual IDataContext DataContext { get; set; }       

        protected DbSet<T> All
        {
            get
            {
                return DataContext.Set<T>();
            }
        }

        public virtual void Add(T dbEntity)
        {
            DataContext.Set<T>().Add(dbEntity);
            DataContext.SaveChanges();
        }

        public virtual void Update(T dbEntity)
        {
            DataContext.SaveChanges();
        }

        public virtual T Get(int id)
        {
            return this.GetWithReferences(id, null);
        }

        public virtual T GetWithReferences(int id, params Expression<Func<T, object>>[] includes)
        {
            var set = DataContext.Set<T>();

            if (includes != null)
            {
                foreach (var expression in includes)
                {
                    set.Include(expression);
                }
            }

            return set.FirstOrDefault(x => x.Id == id);
        }

        public virtual IList<T> GetAll()
        {
            var set = DataContext.Set<T>();

            return set.ToList();
        }

        public virtual void Remove(T dbEntity)
        {
            DataContext.Set<T>().Remove(dbEntity);
            DataContext.SaveChanges();
        }

        public virtual T FirstOrDefault()
        {
            var set = this.DataContext.Set<T>();
            return set.FirstOrDefault(x => true);
        }
    }
}

