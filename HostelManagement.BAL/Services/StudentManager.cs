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

        public async Task<int> AddStudent(Student student)
        {
            IEnumerable<Room> r = await _da.Room.GetAllAsync();
            if (student != null)
            {
                IEnumerable<Student> students = await _da.Student.GetAllAsync();
                if (students.Any(x => x.Id.Equals(student.Id)))
                {
                    return await Task.FromResult(0);
                }
                else if (!(r.Any(x => x.Id.Equals(student.RoomId))))
                {
                    return await Task.FromResult(1);
                }
                else
                {
                    _da.Student.AddAsync(student);
                    _da.Save();
                    return await Task.FromResult(2);
                }
            }
            else
            {
                return await Task.FromResult(-1);
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _da.Student.GetAllAsync();
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            return await _da.Student.GetFirstOrDefaultAsync(x => x.Id == id);
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