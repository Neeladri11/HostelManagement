using AutoMapper;
using HostelManagement.DAL.Models;
using HostelManagement.DAL.View_Models;

namespace HostelManagement.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Hostel, HostelVM>();
            CreateMap<HostelVM, Hostel>();

            CreateMap<Room, RoomVM>();
            CreateMap<RoomVM, Room>();

            CreateMap<Student, StudentVM>();
            CreateMap<StudentVM, Student>();

            CreateMap<Meal, MealVM>();
            CreateMap<MealVM, Meal>();

            CreateMap<Booking, BookingVM>();
            CreateMap<BookingVM, Booking>();

            CreateMap<Payment, PaymentVM>();
            CreateMap<PaymentVM, Payment>();
        }
    }
}
