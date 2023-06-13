using HostelManagement.DAL.Models;

namespace HostelManagement.BAL.Contracts
{
    public interface IStudentManager
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentAsync(int id);
        Task<int> AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Student student);
    }
}