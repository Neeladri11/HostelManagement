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

        public async Task<bool> AddPayment(Payment payment)
        {
            if (payment != null)
            {
                IEnumerable<Payment> payments = await _da.Payment.GetAllAsync();
                if (payments.Any(x => x.PaymentId.Equals(payment.PaymentId)))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var p = new Payment();
                    p.PaymentId = payment.PaymentId;
                    _da.Payment.AddAsync(p);
                    _da.Save();
                }
            }
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _da.Payment.GetAllAsync();
        }

        public async Task<Payment> GetPaymentAsync(int id)
        {
            return await _da.Payment.GetFirstOrDefaultAsync(x => x.PaymentId == id);
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
