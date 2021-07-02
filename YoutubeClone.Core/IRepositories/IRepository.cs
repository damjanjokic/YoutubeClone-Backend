using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace YoutubeClone.Core.IRepositories
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetByIdAsync(object id);

        Task<TEntity> GetSingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity obj);

        Task<bool> SaveAsync();
    }
}