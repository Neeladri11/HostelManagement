using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.DataAccess.Interface;
using HostelManagement.DAL.Models;

namespace HostelManagement.BAL.Services
{
    public class RoomManager : IRoomManager
    {
        private readonly IDataAccess _da;
        public RoomManager(IDataAccess da)
        {
            _da = da;
        }

        public async Task<bool> AddRoom(Room room)
        {
            if (room != null)
            {
                IEnumerable<Room> rooms = await _da.Room.GetAllAsync();
                if (rooms.Any(x => x.RoomId.Equals(room.RoomId)))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var r = new Room();
                    // r.RoomId = room.RoomId;
                    r.RoomStatus = room.RoomStatus;
                    r.FloorNo = room.FloorNo;
                    r.HostelId = room.HostelId;
                    _da.Room.AddAsync(r);
                    _da.Save();
                    return await Task.FromResult(true);
                }
            }
            else
            {
                return false;
            }           
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _da.Room.GetAllAsync();
        }

        public async Task<Room> GetRoomAsync(int RoomId)
        {
            return await _da.Room.GetFirstOrDefaultAsync(x => x.RoomId == RoomId);
        }

        public void UpdateRoom(Room room)
        {
            _da.Room.UpdateExisting(room);
            _da.Save();
        }

        public void DeleteRoom(Room room)
        {
            _da.Room.Remove(room);
            _da.Save();
        }
    }
}