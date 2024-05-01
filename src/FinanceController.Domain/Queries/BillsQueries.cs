using FinanceController.Domain.Entities;
using System.Linq.Expressions;

namespace FinanceController.Domain.Queries
{
    public static class BillsQueries
    {
        public static Expression<Func<Bill, bool>> ListBillsByUserId(Guid userId)
        {
            return x => x.UserId == userId;
        }
    }
}
