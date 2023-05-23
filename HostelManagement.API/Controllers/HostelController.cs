using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelController : ControllerBase
    {
        private readonly IHostelManager _hm;
        public HostelController(IHostelManager hm)
        {
            _hm = hm; ;
        }

        [HttpGet]
        public async Task<IEnumerable<Hostel>> GetAllHostels()
        {
            return await _hm.GetAllHostelsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Hostel> GetHostel(string HostelId)
        {
            return await _hm.GetHostelAsync(HostelId);
        }

        [HttpPost]
        public async Task<IActionResult> AddHostel(Hostel hostel)
         {
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
                 return StatusCode(StatusCodes.Status500InternalServerError, "Error is Adding a New Hostel");
             }
         }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHostel(string HostelId, [FromBody] Hostel hostel)
        {
            try
            {
                if (hostel == null)
                    return BadRequest();
                else
                {
                    Hostel exHostel = await _hm.GetHostelAsync(HostelId);
                    if (exHostel != null)
                    {
                        exHostel.HostelId = hostel.HostelId;
                        _hm.UpdateHostel(exHostel);
                        return Ok("Hostel is updated");
                    }
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating the hostel");
            }
        }
    }
}
