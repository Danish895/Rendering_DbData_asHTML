//using StudentAPI.Models;

using StudentAPI.Models;

namespace StudentAPI.DataAccessLayer.Repository
{
    public interface IUserRepository 
    {
        Task<IEnumerable<StudentDetail>> AllDetails();
        Task<StudentDetail> getById(int id);
        Task<StudentDetail> delete(int id);
        Task<StudentDetail> addThisStudent(StudentDetail studentDetail);
        Task<StudentDetail> updateThisStudent(int id, StudentDetail studentDetail);

    }
}
