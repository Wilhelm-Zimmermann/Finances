namespace FinanceController.Domain.Entities
{
    public class BillType : Base
    {
        public string Type { get; private set; }

        public BillType(string type)
        {
            Type = type;
        }
    }
}
