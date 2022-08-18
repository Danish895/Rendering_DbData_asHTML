using StudentAPI.Models;

namespace StudentAPI.DataAccessLayer.Repository
{
    public interface IUserRepository : IGenericRepository<StudentDetail>
    {
        IUserRepository StudentDetails { get; }

        Task CompleteAsync();
    }
}
