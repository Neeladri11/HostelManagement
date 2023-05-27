using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelController : ControllerBase
    {
        private readonly ILogger<HostelController> _logger;
        private readonly IHostelManager _hm;
        public HostelController(IHostelManager hm, ILogger<HostelController> logger)
        {
            _hm = hm; ;
            _logger = logger;
        }

        /// <summary>
        /// Method to get all hostels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Hostel>> GetAllHostels()
        {
            _logger.LogInformation("GetAllHostels method is called at " + DateTime.Now);
            return await _hm.GetAllHostelsAsync();
        }

        /// <summary>
        /// Method to get hostel by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Hostel> GetHostel(int id)
        {
            _logger.LogInformation("GetHostel method is called at " + DateTime.Now);
            return await _hm.GetHostelAsync(id);
        }

        /// <summary>
        /// Method to add new hostel
        /// </summary>
        /// <param name="hostel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddHostel(Hostel hostel)
         {
            _logger.LogInformation("AddHostel method is called at " + DateTime.Now);
            try
             {
                 if (hostel == null)
                     return BadRequest();
                 else
                 {
                     if (await _hm.AddHostel(hostel))
                         return StatusCode(StatusCodes.Status201Created, "New hostel is created");
                     else
                         return StatusCode(StatusCodes.Status400BadRequest, "Hostel is already available");
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
        public async Task<IActionResult> UpdateHostel(int id, [FromBody] Hostel hostel)
        {
            _logger.LogInformation("UpdateHostel method is called at " + DateTime.Now);
            try
            {
                if (hostel == null)
                    return BadRequest();
                else
                {
                    Hostel exHostel = await _hm.GetHostelAsync(id);
                    if (exHostel != null)
                    {
                        // exHostel.HostelId = hostel.HostelId;
                        exHostel.NoOfStudents = hostel.NoOfStudents;
                        exHostel.NoOfRooms = hostel.NoOfRooms;
                        exHostel.NoOfAvailableRooms = hostel.NoOfAvailableRooms;
                        _hm.UpdateHostel(exHostel);
                        return Ok("Hostel is updated");
                    }
                    return NotFound();
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
                    Hostel delHostel = await _hm.GetHostelAsync(id);
                    if (delHostel == null)
                    { return NotFound("ID does not exist"); }
                    _hm.DeleteHostel(delHostel);
                    return Ok("Hostel Deleted");
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Hostel Details");
            }

        }
    }
}
