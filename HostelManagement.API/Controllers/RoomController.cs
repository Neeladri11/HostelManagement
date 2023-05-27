using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomManager _rm;
        public RoomController(IRoomManager rm, ILogger<RoomController> logger)
        {
            _rm = rm; ;
            _logger = logger;
        }
        
        /// <summary>
        /// Method to get all rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            _logger.LogInformation("GetAllRooms method is called at " + DateTime.Now);
            return await _rm.GetAllRoomsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Room> GetRoom(int id)
        {
            _logger.LogInformation("GetRoom method is called at " + DateTime.Now);
            return await _rm.GetRoomAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(Room room)
        {
            _logger.LogInformation("AddRoom method is called at " + DateTime.Now);
            try
            {
                if (room == null)
                    return BadRequest();
                else
                {
                    if (await _rm.AddRoom(room))
                        return StatusCode(StatusCodes.Status201Created, "New room is created");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "Room is already available");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error is Adding a New Room");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room room)
        {
            _logger.LogInformation("UpdateRoom method is called at " + DateTime.Now);
            try
            {
                if (room == null)
                    return BadRequest();
                else
                {
                    Room exRoom = await _rm.GetRoomAsync(id);
                    if (exRoom != null)
                    {
                        exRoom.RoomId = room.RoomId;
                        exRoom.RoomStatus = room.RoomStatus;
                        exRoom.FloorNo = room.FloorNo;
                        exRoom.HostelId = room.HostelId;
                        _rm.UpdateRoom(exRoom);
                        return Ok("Room is updated");
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating the room");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
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
                    Room delRoom = await _rm.GetRoomAsync(id);
                    if (delRoom == null)
                    { return NotFound("ID does not exist"); }
                    _rm.DeleteRoom(delRoom);
                    return Ok("Room Deleted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Room Details");
            }

        }
    }
}
