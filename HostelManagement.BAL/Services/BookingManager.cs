using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.DataAccess.Interface;
using HostelManagement.DAL.Models;

namespace HostelManagement.BAL.Services
{
    public class BookingManager : IBookingManager
    {
        private readonly IDataAccess _da;
        public BookingManager(IDataAccess da)
        {
            _da = da;
        }

        public async Task<int> AddBooking(Booking booking)
        {
            IEnumerable<Student> s = await _da.Student.GetAllAsync();
            if (booking != null)
            {
                IEnumerable<Booking> bookings = await _da.Booking.GetAllAsync();
                if (bookings.Any(x => x.Id.Equals(booking.Id)))
                {
                    return await Task.FromResult(0);
                }
                else if (!(s.Any(x => x.Id.Equals(booking.StudentId))))
                {
                    return await Task.FromResult(1);
                }
                else
                {
                    _da.Booking.AddAsync(booking);
                    _da.Save();
                    return await Task.FromResult(2);
                }
            }
            else
            {
                return await Task.FromResult(-1);
            }
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _da.Booking.GetAllAsync();
        }

        public async Task<Booking> GetBookingAsync(int id)
        {
            return await _da.Booking.GetFirstOrDefaultAsync(x => x.Id == id);

        }

        public void UpdateBooking(Booking booking)
        {
            _da.Booking.UpdateExisting(booking);
            _da.Save();
        }

        public void DeleteBooking(Booking booking)
        {
            _da.Booking.Remove(booking);
            _da.Save();
        }
    }  
}