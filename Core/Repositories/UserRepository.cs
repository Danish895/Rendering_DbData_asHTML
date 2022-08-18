using Microsoft.EntityFrameworkCore;
using StudentAPI.Core.IRepositories;
using StudentAPI.Models;

namespace StudentAPI.Core.Repositories
{
    public class UserRepository : GenericRepository<StudentDetail> , IUserRepository
    {
        public UserRepository(
            StudentContext context,
            ILogger  logger
        ) : base(context, logger)
        {
        }
        public override async Task<IEnumerable<StudentDetail>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(UserRepository));
                return new List<StudentDetail>();
            }
        }

        public override async Task<bool> Upsert(StudentDetail entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();
                if (existingUser == null)
                return await Add(entity);

                existingUser.Name = entity.Name;
                existingUser.Address = entity.Address;
                existingUser.IsCreatedAt = entity.IsCreatedAt;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert method error", typeof(UserRepository));
                return false;
            }
        }
        public override async Task<bool> Delete(int id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
                if (exist != null)
                {
                    dbSet.Remove(exist);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete method error", typeof(UserRepository));
                return false;
            }
        }
    }
}
