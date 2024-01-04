using FinanceController.Domain.Entities;

namespace FinanceController.Domain.Repositories.Contracts
{
    public interface IBillRepository
    {
        Task CreateBill(Bill command);
        Task<IEnumerable<Bill>> GetAllBills();
        Task DeleteBill(Guid id);
    }
}
