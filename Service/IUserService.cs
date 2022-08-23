using StudentAPI.DataAccessLayer.Repository;
using StudentAPI.Models;

namespace StudentAPI.Service
{
    public interface IUserService
    {
        //IUserRepository Studentdetails { get; }

        Task<IEnumerable<StudentDetail>> All();
        Task<StudentDetail> getStudentById(int id);
        Task<StudentDetail> deleteById(int id);
        Task<StudentDetail> addStudent(StudentDetail studentDetail);
        Task<StudentDetail> update(int id, StudentDetail studentDetail);
        Task CompleteAsync();
    }
}
