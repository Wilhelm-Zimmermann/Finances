using FinanceController.Domain.Entities;
using FinanceController.Domain.Infra.Contexts;
using FinanceController.Domain.Repositories.Contracts;

namespace FinanceController.Domain.Infra.Repositories
{
    public class BillTypeRepository : IBillTypeRepository
    {
        private DatabaseContext _context;

        public BillTypeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateBill(BillType billType)
        {
            _context.BillTypes.Add(billType);
            await _context.SaveChangesAsync();
        }
    }
}
