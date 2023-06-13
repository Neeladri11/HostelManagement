using AutoMapper;
using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.Models;
using HostelManagement.DAL.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentManager _sm;
        private readonly IMapper _mapper;
        public StudentController(IMapper mapper, IStudentManager sm, ILogger<StudentController> logger)
        {
            _sm = sm ;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to get all students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<StudentVM>> GetAllStudents()
        {
            _logger.LogInformation("GetAllStudents method is called at " + DateTime.Now);
            
            IEnumerable<Student> students = await _sm.GetAllStudentsAsync();
            var svmList = students.Select(students => _mapper.Map<StudentVM>(students));
            return svmList;
        }

        [HttpGet("{id}")]
        public async Task<StudentVM> GetStudent(int id)
        {
            _logger.LogInformation("GetStudent method is called at " + DateTime.Now);
            
            Student student = await _sm.GetStudentAsync(id);
            var svm = _mapper.Map<StudentVM>(student);
            return svm;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentVM svm)
        {
            _logger.LogInformation("AddStudent method is called at " + DateTime.Now);
            
            try
            {
                if (svm == null)
                    return BadRequest();
                else
                {
                    Student student = _mapper.Map<Student>(svm);
                    var check = await _sm.AddStudent(student);
                    if (check == 0)
                        return StatusCode(StatusCodes.Status400BadRequest, "Student already exists");
                    else if (check == 1)
                        return StatusCode(StatusCodes.Status400BadRequest, "Foreign key values are not correct");
                    else if (check == -1)
                        return StatusCode(StatusCodes.Status400BadRequest, "The Student object entered is empty");
                    else
                        return StatusCode(StatusCodes.Status201Created, "New Student is created");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error is Adding a New Student");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentVM svm)
        {
            _logger.LogInformation("UpdateStudent method is called at " + DateTime.Now);
            try
            {
                if (svm == null)
                    return BadRequest();
                else
                {
                    Student exStudent = await _sm.GetStudentAsync(id);
                    if (exStudent == null)
                    {
                        return NotFound("Id does not exist");
                    }
                    Student student = _mapper.Map<StudentVM, Student>(svm, exStudent);
                    _sm.UpdateStudent(student);
                    return Ok("Student is updated");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating the student");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                if (id == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Student delStudent = await _sm.GetStudentAsync(id);
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