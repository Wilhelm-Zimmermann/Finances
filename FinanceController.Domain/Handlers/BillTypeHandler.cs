
using FinanceController.Domain.Commands;
using FinanceController.Domain.Commands.Contracts;
using FinanceController.Domain.Entities;
using FinanceController.Domain.Handlers.Contracts;

namespace FinanceController.Domain.Handlers
{
    public class BillTypeHandler : IHandler<CreateBillTypeCommand>
    {
        public async Task<ICommandResult> Handle(CreateBillTypeCommand command)
        {
            var billType = new BillType(command.Type);

            return new GenericCommandResult(true, "Bill Type created successfully", billType);
        }
    }
}
