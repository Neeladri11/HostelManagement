using HostelManagement.DAL.Models;

namespace HostelManagement.BAL.Contracts
{
    public interface IBookingManager
    {
            Task<IEnumerable<Booking>> GetAllBookingsAsync();
            Task<Booking> GetBookingAsync(int id);
            Task<bool> AddBooking(Booking booking);
            void UpdateBooking(Booking booking);
            void DeleteBooking(Booking booking);
    }
}
