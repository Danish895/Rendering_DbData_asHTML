using StudentAPI.Core.IRepositories;

namespace StudentAPI.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository StudentDetails { get; }

        Task CompleteAsync();
    }
}
