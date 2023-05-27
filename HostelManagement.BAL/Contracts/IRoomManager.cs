using HostelManagement.DAL.Models;

namespace HostelManagement.BAL.Contracts
{
    public interface IRoomManager
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room> GetRoomAsync(int id);
        Task<bool> AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(Room room);
    }
}