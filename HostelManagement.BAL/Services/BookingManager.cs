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

        public async Task<bool> AddBooking(Booking booking)
        {
            if (booking != null)
            {
                IEnumerable<Booking> bookings = await _da.Booking.GetAllAsync();
                if (bookings.Any(x => x.BookingId.Equals(booking.BookingId)))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var bk = new Booking();
                    bk.BookingId = booking.BookingId;
                    _da.Booking.AddAsync(bk);
                    _da.Save();
                }
            }
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _da.Booking.GetAllAsync();
        }

        public async Task<Booking> GetBookingAsync(int BookingId)
        {
            return await _da.Booking.GetFirstOrDefaultAsync(x => x.BookingId == BookingId);

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
