using Microsoft.EntityFrameworkCore;
using StudentAPI.DataAccessLayer.Context;
using StudentAPI.Models;

namespace StudentAPI.DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected StudentContext _context;
        protected DbSet<T> dbSet;
       // protected readonly ILogger _logger;

        public GenericRepository(
            StudentContext context
            //ILogger logger
        )
        {
            _context = context;
            //_logger = logger;
            this.dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(int id)
        {
            throw new NotImplementedException();

        }

        public virtual Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
