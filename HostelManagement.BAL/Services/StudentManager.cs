using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.DataAccess.Interface;
using HostelManagement.DAL.Models;

namespace HostelManagement.BAL.Services
{
    public class StudentManager : IStudentManager
    {
        private readonly IDataAccess _da;
        public StudentManager(IDataAccess da)
        {
            _da = da;
        }

        public async Task<bool> AddStudent(Student student)
        {
            if (student != null)
            {
                IEnumerable<Student> students = await _da.Student.GetAllAsync();
                if (students.Any(x => x.StudentId.Equals(student.StudentId)))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var stud = new Student();
                    stud.StudentId = student.StudentId;
                    _da.Student.AddAsync(stud);
                    _da.Save();
                }
            }
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _da.Student.GetAllAsync();
        }

        public async Task<Student> GetStudentAsync(int StudentId)
        {
            return await _da.Student.GetFirstOrDefaultAsync(x => x.StudentId == StudentId);
        }

        public void UpdateStudent(Student student)
        {
            _da.Student.UpdateExisting(student);
            _da.Save();
        }

        public void DeleteStudent(Student student)
        {
            _da.Student.Remove(student);
            _da.Save();
        }
    }
}
