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
                if (hostels.Any(x => x.HostelId.Equals(hostel.HostelId)))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var h = new Hostel();
                    h.HostelId = hostel.HostelId;
                    _da.Hostel.AddAsync(h);
                    _da.Save();
                }
            }
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Hostel>> GetAllHostelsAsync()
        {
            return await _da.Hostel.GetAllAsync();
        }

        public async Task<Hostel> GetHostelAsync(string HostelId)
        {
            return await _da.Hostel.GetFirstOrDefaultAsync(x=>x.HostelId == HostelId);
        }

        public void UpdateHostel(Hostel hostel)
        {
            _da.Hostel.UpdateExisting(hostel);
            _da.Save();
        }
    }
}
