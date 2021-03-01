using BlogCore.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Repository
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey>: IRepositoryBase<TEntity, TPrimaryKey> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        public DefaultContext _dbContext { get; } = null;
        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string _connectionString { get; set; }

        public RepositoryBase(DefaultContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public DatabaseFacade Database => _dbContext.Database;
        public IQueryable<TEntity> Entities => _dbSet.AsQueryable().AsNoTracking();
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public bool Any(Expression<Func<TEntity, bool>> whereLambd)
        {
            return _dbSet.Where(whereLambd).Any();
        }

        #region 查找
        public long Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return _dbSet.LongCount(predicate);
        }
        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return await _dbSet.LongCountAsync(predicate);
        }
        public TEntity Get(TPrimaryKey id)
        {
            if (id == null)
            {
                return default(TEntity);
            }
            return _dbSet.Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate = null, bool isNoTracking = true)
        {
            var data = isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);
            return data.FirstOrDefault();
        }


        public async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            if (id == null)
            {
                return default(TEntity);
            }
            return await _dbSet.FindAsync(id);
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, bool isNoTracking = true)
        {
            var data = isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);
            return await data.FirstOrDefaultAsync();
        }


        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, string ordering = "", bool isNoTracking = true)
        {
            var data = isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);
            //if (!string.IsNullOrEmpty(ordering))
            //{
            //    data = data.OrderBy(ordering);
            //}
            return await data.ToListAsync();
        }
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null, string ordering = "", bool isNoTracking = true)
        {
            var data = isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);
            //if (!string.IsNullOrEmpty(ordering))
            //{
            //    data = data.OrderBy(ordering);
            //}
            return data.ToList();
        }
        public async Task<IQueryable<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> predicate = null, bool isNoTracking = true)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return await Task.Run(() => isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate));
        }
        public IQueryable<TEntity> Load(Expression<Func<TEntity, bool>> predicate = null, bool isNoTracking = true)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);
        }

        public void Dispose()
        {
            this._dbContext.Dispose();
        }
        #endregion
    }
}
