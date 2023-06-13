using HostelManagement.DAL.Models;

namespace HostelManagement.DAL.DataAccess.Interface
{
    public interface IDataAccess
    {
        IHostelRepo Hostel { get; }
        IRoomRepo Room { get; }
        IStudentRepo Student { get; }
        IMealRepo Meal { get; }
        IBookingRepo Booking { get; }
        IPaymentRepo Payment { get; }
        void Save();
    }

    //Individual Model Repos
    public interface IHostelRepo : IRepo<Hostel> { }
    public interface IRoomRepo : IRepo<Room> { }
    public interface IStudentRepo : IRepo<Student> { }
    public interface IMealRepo : IRepo<Meal> { }
    public interface IBookingRepo : IRepo<Booking> { }
    public interface IPaymentRepo : IRepo<Payment> { }
}
