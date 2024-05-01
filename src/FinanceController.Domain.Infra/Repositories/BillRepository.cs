using FinanceController.Domain.Entities;
using FinanceController.Domain.Infra.Contexts;
using FinanceController.Domain.Queries;
using FinanceController.Domain.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FinanceController.Domain.Infra.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly DatabaseContext _context;

        public BillRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateBill(Bill command)
        {
            _context.Bills.Add(command);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBill(Guid id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if(bill != null)
            {
                _context.Bills.Remove(bill);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Bill>> GetAllBills()
        {
            return await _context.Bills.ToListAsync();
        }

        public async Task<IEnumerable<Bill>> ListBillsByUserId(Guid userId)
        {
            return await _context.Bills.Where(BillsQueries.ListBillsByUserId(userId)).ToListAsync();
        }
    }
}
