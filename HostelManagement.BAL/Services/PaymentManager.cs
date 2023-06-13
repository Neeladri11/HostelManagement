using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.DataAccess.Interface;
using HostelManagement.DAL.Models;

namespace HostelManagement.BAL.Services
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IDataAccess _da;
        public PaymentManager(IDataAccess da)
        {
            _da = da;
        }

        public async Task<int> AddPayment(Payment payment)
        {
            IEnumerable<Booking> b = await _da.Booking.GetAllAsync();
            if (payment != null)
            {
                IEnumerable<Payment> payments = await _da.Payment.GetAllAsync();
                if (payments.Any(x => x.Id.Equals(payment.Id)))
                {
                    return await Task.FromResult(0);
                }
                else if (!(b.Any(x => x.Id.Equals(payment.BookingId))))
                {
                    return await Task.FromResult(1);
                }
                else
                {
                    _da.Payment.AddAsync(payment);
                    _da.Save();
                    return await Task.FromResult(2);
                }
            }
            else
            {
                return await Task.FromResult(-1);
            }
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _da.Payment.GetAllAsync();
        }

        public async Task<Payment> GetPaymentAsync(int id)
        {
            return await _da.Payment.GetFirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdatePayment(Payment payment)
        {
            _da.Payment.UpdateExisting(payment);
            _da.Save();
        }

        public void DeletePayment(Payment payment)
        {
            _da.Payment.Remove(payment);
            _da.Save();
        }
    }
}
