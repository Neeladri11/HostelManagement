using HostelManagement.DAL.Models;

namespace HostelManagement.BAL.Contracts
{
    public interface IPaymentManager
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentAsync(int id);
        Task<bool> AddPayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(Payment payment);
    }
}
