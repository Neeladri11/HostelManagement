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

        public async Task<int> AddRoom(Room room)
        {
            IEnumerable<Hostel> h = await _da.Hostel.GetAllAsync();
            if (room != null)
            {
                IEnumerable<Room> rooms = await _da.Room.GetAllAsync();
                if (rooms.Any(x => x.Id.Equals(room.Id)))
                {
                    return await Task.FromResult(0);
                }
                else if (!(h.Any(x=>x.Id.Equals(room.HostelId))))
                {
                    return await Task.FromResult(1);
                }
                else
                {
                    //var r = new Room();
                    //r.RoomId = room.RoomId;
                    //r.RoomStatus = room.RoomStatus;
                    //r.FloorNo = room.FloorNo;
                    //r.HostelId = room.HostelId;
                    _da.Room.AddAsync(room);
                    _da.Save();
                    return await Task.FromResult(2);
                }
            }
            else
            {
                return await Task.FromResult(-1);
            }           
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _da.Room.GetAllAsync();
        }

        public async Task<Room> GetRoomAsync(int id)
        {
            return await _da.Room.GetFirstOrDefaultAsync(x => x.Id == id);
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