using AutoMapper;
using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.Models;
using HostelManagement.DAL.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomManager _rm;
        private readonly IMapper _mapper;
        public RoomController(IMapper mapper,IRoomManager rm, ILogger<RoomController> logger)
        {
            _rm = rm; ;
            _logger = logger;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Method to get all rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<RoomVM>> GetAllRooms()
        {
            _logger.LogInformation("GetAllRooms method is called at " + DateTime.Now);
            
            IEnumerable<Room> rooms = await _rm.GetAllRoomsAsync();
            var rvm = rooms.Select(rooms => _mapper.Map<RoomVM>(rooms));
            return rvm;
        }

        [HttpGet("{id}")]
        public async Task<RoomVM> GetRoom(int id)
        {
            _logger.LogInformation("GetRoom method is called at " + DateTime.Now);
            
            Room room = await _rm.GetRoomAsync(id);
            var rvm = _mapper.Map<RoomVM>(room);
            return rvm;
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(RoomVM room)
        {
            _logger.LogInformation("AddRoom method is called at " + DateTime.Now);
            
            try
            {
                if (room == null)
                    return BadRequest();
                else
                {
                    Room r = _mapper.Map<Room>(room);
                    var check = await _rm.AddRoom(r);
                    if(check == 0)
                        return StatusCode(StatusCodes.Status400BadRequest, "Room already exists");
                    else if(check == 1)
                        return StatusCode(StatusCodes.Status400BadRequest, "Foreign key values are not correct");
                    else if(check == -1)
                        return StatusCode(StatusCodes.Status400BadRequest, "The Room object entered is empty");
                    else
                        return StatusCode(StatusCodes.Status201Created, "New room is created");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error is Adding a New Room");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomVM room)
        {
            _logger.LogInformation("UpdateRoom method is called at " + DateTime.Now);
            
            try
            {
                if (room == null)
                    return BadRequest();
                else
                {
                    Room exRoom = await _rm.GetRoomAsync(id);
                    if (exRoom == null)
                    {
                        return NotFound("Id does not exist");
                    }
                    var r = _mapper.Map<RoomVM, Room>(room,exRoom);
                    //exRoom.RoomId = room.RoomId;
                    //exRoom.RoomStatus = room.RoomStatus;
                    //exRoom.FloorNo = room.FloorNo;
                    //exRoom.HostelId = room.HostelId;
                    _rm.UpdateRoom(r);
                    return Ok("Room is updated");
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
