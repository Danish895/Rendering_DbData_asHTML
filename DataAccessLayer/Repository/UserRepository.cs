using Microsoft.EntityFrameworkCore;
using StudentAPI.DataAccessLayer.Context;
using StudentAPI.Models;


namespace StudentAPI.DataAccessLayer.Repository
{
    public  class UserRepository : IUserRepository
    {
        private readonly StudentContext _context;
        public UserRepository(StudentContext context)
        {
            _context = context;
        }

        public  async Task<IEnumerable<StudentDetail>> AllDetails()
        {
            //try
            //{
                return await _context.StudentDetails.ToListAsync();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error hai in calling all method");
            //    Console.WriteLine(ex.Message);
                
            //    //_logger.LogError(ex, "{Repo} All method error", typeof(UserRepository));
            //    return new List<StudentDetail>() { };

            //}
        }  
        public async Task<StudentDetail> getById(int id)
        {
            return await _context.StudentDetails.FindAsync(id);
        }

        public async Task<StudentDetail> delete(int id)
        {
            StudentDetail studentToBeDeleted = await _context.StudentDetails.Where(x => x.Id == id).FirstOrDefaultAsync();
             _context.StudentDetails.Remove(studentToBeDeleted);
            await _context.SaveChangesAsync();
            return await _context.StudentDetails.FindAsync(id);
        }

        public async Task<StudentDetail> addThisStudent(StudentDetail studentDetail)
        {
              _context.StudentDetails.Add(studentDetail);
            await _context.SaveChangesAsync();
            return await _context.StudentDetails.FindAsync(studentDetail.Id);

        }
        public async Task<StudentDetail> updateThisStudent(int id, StudentDetail studentDetail)
        {
            StudentDetail studentToBeUpdated = await _context.StudentDetails.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (studentDetail.Name != null) { studentToBeUpdated.Name = studentDetail.Name.Trim(); }
            if (studentDetail.Address != null) { studentToBeUpdated.Address = studentDetail.Address.Trim(); }
            //_context.StudentDetails.Add(studentDetail);
            await _context.SaveChangesAsync();
            return await _context.StudentDetails.FindAsync(id);
        }
    }
}
