using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentManager _sm;
        public StudentController(IStudentManager sm, ILogger<StudentController> logger)
        {
            _sm = sm ;
            _logger = logger;
        }

        /// <summary>
        /// Method to get all students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            _logger.LogInformation("GetAllStudents method is called at " + DateTime.Now);
            return await _sm.GetAllStudentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Student> GetStudent(int studentId)
        {
            _logger.LogInformation("GetStudent method is called at " + DateTime.Now);
            return await _sm.GetStudentAsync(studentId);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            _logger.LogInformation("AddStudent method is called at " + DateTime.Now);
            try
            {
                if (student == null)
                    return BadRequest();
                else
                {
                    if (await _sm.AddStudent(student))
                        return StatusCode(StatusCodes.Status201Created, "New student is created");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "Student is already available");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error is Adding a New Student");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int studentId, [FromBody] Student student)
        {
            _logger.LogInformation("UpdateStudent method is called at " + DateTime.Now);
            try
            {
                if (student == null)
                    return BadRequest();
                else
                {
                    Student exStudent = await _sm.GetStudentAsync(studentId);
                    if (exStudent != null)
                    {
                        exStudent.StudentId = student.StudentId;
                        _sm.UpdateStudent(exStudent);
                        return Ok("Student is updated");
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating the student");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            try
            {
                if (studentId == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Student delStudent = await _sm.GetStudentAsync(studentId);
                    if (delStudent == null)
                    { return NotFound("ID does not exist"); }
                    _sm.DeleteStudent(delStudent);
                    return Ok("Student Deleted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Student Details");
            }

        }
    }
}