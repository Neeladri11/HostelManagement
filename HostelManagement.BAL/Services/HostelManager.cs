using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.DataAccess.Interface;
using HostelManagement.DAL.Models;

namespace HostelManagement.BAL.Services
{
    public class HostelManager : IHostelManager
    {
        private readonly IDataAccess _da;
        public HostelManager(IDataAccess da)
        {
            _da = da;
        }

        public async Task<bool> AddHostel(Hostel hostel)
        {
            if (hostel != null)
            {
                IEnumerable<Hostel> hostels = await _da.Hostel.GetAllAsync();
                if (hostels.Any(x => x.Id.Equals(hostel.Id)))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                   /* var h = new Hostel();
                    h.Id = hostel.Id;
                    h.NoOfStudents = hostel.NoOfStudents;
                    h.NoOfRooms = hostel.NoOfRooms;
                    h.NoOfAvailableRooms = hostel.NoOfAvailableRooms;*/
                    _da.Hostel.AddAsync(hostel);
                    _da.Save();
                    return await Task.FromResult(true);
                }
            }
            else
            {
                return false;
            }

        }

        public async Task<IEnumerable<Hostel>> GetAllHostelsAsync()
        {
            return await _da.Hostel.GetAllAsync();
        }

        public async Task<Hostel> GetHostelAsync(int id)
        {
            return await _da.Hostel.GetFirstOrDefaultAsync(x=>x.Id == id);
        }

        public void UpdateHostel(Hostel hostel)
        {
            _da.Hostel.UpdateExisting(hostel);
            _da.Save();
        }

        public void DeleteHostel(Hostel hostel)
        {
            _da.Hostel.Remove(hostel);
            _da.Save();
        }
    }
}