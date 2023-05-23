using HostelManagement.DAL.Models;
using HostelManagement.DAL.DataAccess.Interface;
using HostelManagement.DAL.Data;

namespace HostelManagement.DAL.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly ApplicationDbContext _db;
        public DataAccess(ApplicationDbContext db)
        {
            _db = db;
            Hostel = new HostelRepo(_db);
            Room = new RoomRepo(_db);
            Student = new StudentRepo(_db);
            Meal = new MealRepo(_db);
            Booking = new BookingRepo(_db);
            Payment = new PaymentRepo(_db);
        }

        public IHostelRepo Hostel { get; private set; }

        public IRoomRepo Room { get; private set; }

        public IStudentRepo Student { get; private set; }

        public IMealRepo Meal { get; private set; }

        public IBookingRepo Booking { get; private set; }

        public IPaymentRepo Payment { get; private set; }

        public void Save()
        {
            _db.SaveChangesAsync();
        }
    }

    public class HostelRepo : Repo<Hostel>, IHostelRepo
    {
        private readonly ApplicationDbContext _db;
        public HostelRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }

    public class RoomRepo : Repo<Room>, IRoomRepo
    {
        private readonly ApplicationDbContext _db;
        public RoomRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }

    public class StudentRepo : Repo<Student>, IStudentRepo
    {
        private readonly ApplicationDbContext _db;
        public StudentRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }

    public class MealRepo : Repo<Meal>, IMealRepo
    {
        private readonly ApplicationDbContext _db;
        public MealRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }

    public class BookingRepo : Repo<Booking>, IBookingRepo
    {
        private readonly ApplicationDbContext _db;
        public BookingRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }

    public class PaymentRepo : Repo<Payment>, IPaymentRepo
    {
        private readonly ApplicationDbContext _db;
        public PaymentRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
