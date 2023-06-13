using AutoMapper;
using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.Models;
using HostelManagement.DAL.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelController : ControllerBase
    {
        private readonly ILogger<HostelController> _logger;
        private readonly IHostelManager _hm;
        private readonly IMapper _mapper;
        
        public HostelController(IHostelManager hm, ILogger<HostelController> logger, IMapper mapper)
        {
            _hm = hm; ;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to get all hostels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<HostelVM>> GetAllHostels()
        {
            _logger.LogInformation("GetAllHostels method is called at " + DateTime.Now);
            IEnumerable<Hostel> hostels = await _hm.GetAllHostelsAsync();
            var hvm = hostels.Select(hostels => _mapper.Map<HostelVM>(hostels));
            return hvm;
        }

        /// <summary>
        /// Method to get hostel by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<HostelVM> GetHostel(int id)
        {
            _logger.LogInformation("GetHostel method is called at " + DateTime.Now);
            Hostel hostel = await _hm.GetHostelAsync(id);
            var hvm = _mapper.Map<HostelVM>(hostel);
            return hvm;
        }

        /// <summary>
        /// Method to add new hostel
        /// </summary>
        /// <param name="hostel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddHostel(HostelVM hvm)
         {
            _logger.LogInformation("AddHostel method is called at " + DateTime.Now);
            try
             {
                 if (hvm == null)
                     return BadRequest();
                 else
                 {
                    Hostel hostel = _mapper.Map<Hostel>(hvm);
                     if (await _hm.AddHostel(hostel))
                         return StatusCode(StatusCodes.Status201Created, "New hostel is created");
                     else
                         return StatusCode(StatusCodes.Status400BadRequest, "Hostel already exists");
                 }
             }
             catch(Exception ex)
             {
                 _logger.LogError(ex, ex.Message);
                 return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding a new hostel");
             }
         }

        /// <summary>
        /// Method to update existing hostel
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hostel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHostel(int id, [FromBody] HostelVM hvm)
        {
            _logger.LogInformation("UpdateHostel method is called at " + DateTime.Now);
            try
            {
                if (hvm == null)
                    return BadRequest();
                else
                {
                    Hostel exHostel = await _hm.GetHostelAsync(id);
                    if (exHostel == null)
                    {
                        return NotFound("Id does not exist");
                    }
                    else
                    {
                        /* exHostel.HostelId = hostel.HostelId;
                        exHostel.NoOfStudents = hostel.NoOfStudents;
                        exHostel.NoOfRooms = hostel.NoOfRooms;
                        exHostel.NoOfAvailableRooms = hostel.NoOfAvailableRooms; */
                        Hostel hostel = _mapper.Map<HostelVM, Hostel>(hvm, exHostel);
                        _hm.UpdateHostel(hostel);
                        return Ok("Hostel is updated");
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating the hostel");
            }
        }
        /// <summary>
        /// Method to delete hostel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHostel(int id)
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
                    Hostel delHostel = await _hm.GetHostelAsync(id);
                    if (delHostel == null)
                    { return NotFound("ID does not exist"); }
                    _hm.DeleteHostel(delHostel);
                    return Ok("Hostel Deleted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Hostel Details");
            }

        }
    }
}