using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

using WCS.DataLayer.Contex;
using WCS.InterfaceService.InterfacesBase;

namespace WCS.Service.ServiceBase
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly ContextWCS _ContextWCS;
        private readonly DbSet<T> _DbSet;

        public BaseService(ContextWCS ContextWCS)
        {
            _ContextWCS = ContextWCS;
            _DbSet = _ContextWCS.Set<T>();
        }
         
        public virtual async Task<bool> Exists(Expression<Func<T, bool>> IDExpression)
        {
            bool exits = await _DbSet.AnyAsync(IDExpression).ConfigureAwait(false);
            return exits;
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _DbSet.ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<List<T>> GetAll(Expression<Func<T, bool>> IDExpression)
        {
            return await _DbSet.Where(IDExpression).ToListAsync().ConfigureAwait(false);
        }


        public virtual Task<List<T>> GetAll_SP()
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetByID_SP(int ID)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetByWhere(Expression<Func<T, bool>> IDExpression)
        {
            return await _DbSet.FirstOrDefaultAsync(IDExpression).ConfigureAwait(false);
        }
        public virtual async Task<bool> Insert(T entity)
        {
            _ContextWCS.ChangeTracker.Clear();
            await _DbSet.AddAsync(entity).ConfigureAwait(false);
            return true;
        }

        public virtual async Task<bool> InsertRange(List<T> entitis)
        {
            _ContextWCS.ChangeTracker.Clear();
            await _DbSet.AddRangeAsync(entitis).ConfigureAwait(false);
            return true;
        }
        public virtual bool Update(T entity)
        {
            _ContextWCS.ChangeTracker.Clear();
            _DbSet.Update(entity);
            return true;
        }

        public bool UpdateRange(List<T> entitis)
        {
            _ContextWCS.ChangeTracker.Clear();
            _DbSet.UpdateRange(entitis);
            return true;
        }

        public virtual bool Delete(T entity)
        {
            _ContextWCS.ChangeTracker.Clear();
            _DbSet.Remove(entity);
            return true;
        }

        public virtual bool DeleteRange(List<T> entitis)
        {
            _ContextWCS.ChangeTracker.Clear();
            _DbSet.RemoveRange(entitis);
            return true;
        }

    }
}
