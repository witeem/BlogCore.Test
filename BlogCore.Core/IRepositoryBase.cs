using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Core
{
    public interface IRepositoryBase<TEntity, TKey> : IDisposable where TEntity : class
    {
        #region 查询
        long Count(Expression<Func<TEntity, bool>> predicate = null);
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        TEntity Get(Expression<Func<TEntity, bool>> predicate, bool isNoTracking);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool isNoTracking);
        Task<TEntity> GetAsync(TKey id);

        IQueryable<TEntity> Load(Expression<Func<TEntity, bool>> predicate, bool isNoTracking);
        Task<IQueryable<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> predicate, bool isNoTracking);

        List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, string ordering, bool isNoTracking);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, string ordering, bool isNoTracking);
        #endregion
    }
}
