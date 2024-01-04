using FinanceController.Domain.Commands;
using FinanceController.Domain.Commands.Contracts;
using FinanceController.Domain.Entities;
using FinanceController.Domain.Handlers.Contracts;
using FinanceController.Domain.Repositories.Contracts;

namespace FinanceController.Domain.Handlers
{
    public class BillHandler : IHandler<CreateBillCommand>
    {
        private readonly IBillRepository _billRepository;

        public BillHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }
        public async Task<ICommandResult> Handle(CreateBillCommand command)
        {
            var bill = new Bill(command.Name, command.Price, command.Description, command.PaidDate, command.BillTypeId);
            await _billRepository.CreateBill(bill);
            return new GenericCommandResult(true, "Bill created sucessfully", command);
        }
    }
}
