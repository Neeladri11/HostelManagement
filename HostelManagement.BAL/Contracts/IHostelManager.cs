using HostelManagement.DAL.Models;

namespace HostelManagement.BAL.Contracts
{
    public interface IHostelManager
    {
        Task<IEnumerable<Hostel>> GetAllHostelsAsync();
        Task<Hostel> GetHostelAsync(int id);
        Task<bool> AddHostel(Hostel hostel);
        void UpdateHostel(Hostel hostel);
        void DeleteHostel(Hostel hostel);
    }
}
