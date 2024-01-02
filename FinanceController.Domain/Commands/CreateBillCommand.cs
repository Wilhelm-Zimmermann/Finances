using FinanceController.Domain.Entities;

namespace FinanceController.Domain.Commands
{
    public class CreateBillCommand
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime PaidDate { get; set; }

        public Guid BillTypeId { get; set; }

        public CreateBillCommand(string name, double price, string description, DateTime paidDate, Guid billTypeId)
        {
            Name = name;
            Price = price;
            Description = description;
            PaidDate = paidDate;
            BillTypeId = billTypeId;
        }
    }
}
