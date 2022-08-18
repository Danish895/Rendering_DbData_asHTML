using StudentAPI.Core.IConfiguration;
using StudentAPI.Core.IRepositories;
using StudentAPI.Core.Repositories;

namespace StudentAPI.Models
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly StudentContext _context;
        private readonly ILogger _logger;
        public IUserRepository StudentDetails { get; private set; }

        public UnitOfWork(
            StudentContext context,
            ILoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            StudentDetails = new UserRepository(_context, _logger);
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
