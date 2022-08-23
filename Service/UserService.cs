using Microsoft.EntityFrameworkCore;
using StudentAPI.DataAccessLayer.Context;
using StudentAPI.DataAccessLayer.Repository;
using StudentAPI.Models;

namespace StudentAPI.Service
{
    public class UserService : IUserService
    {
        private readonly StudentContext _context;
        public IUserRepository Studentdetails { get;  set; }
        private IUserRepository _UserRepository;
        public UserService( StudentContext context, IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
           _context = context;
            Studentdetails = new UserRepository(_context);
        }
        public async Task<IEnumerable<StudentDetail>> All()
        {
            var detail = await _UserRepository.AllDetails();
            return detail;
        }
        public async Task<StudentDetail> getStudentById(int id)
        {
            var detail = await _UserRepository.getById(id);
            return detail;
        }

        public async Task<StudentDetail> deleteById(int id)
        {
            var detail = await _UserRepository.delete(id);
            return detail;
        }

        public async Task<StudentDetail> addStudent(StudentDetail studentDetail)
        {
            var detail = await _UserRepository.addThisStudent(studentDetail);
            return detail;
        }
        public async Task<StudentDetail> update(int id ,StudentDetail studentDetail)
        {
            var detail = await _UserRepository.updateThisStudent(id, studentDetail);
            return detail;
        }


        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
