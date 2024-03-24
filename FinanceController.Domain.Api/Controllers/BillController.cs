using FinanceController.Domain.Api.Utils;
using FinanceController.Domain.Commands;
using FinanceController.Domain.Handlers;
using FinanceController.Domain.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceController.Domain.Api.Controllers
{
    [ApiController]
    [Route("api/v1/bills")]
    public class BillController : ControllerBase
    {
        [HttpPost]
        [Route("create/{billTypeId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<GenericCommandResult>> CreateBill([FromBody] CreateBillCommand command, [FromServices] BillHandler handler, Guid billTypeId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId") ?? throw new NullReferenceException();
            command.BillTypeId = billTypeId;
            command.UserId = Guid.Parse(userId.Value);
            var result = await handler.Handle(command);

            return StatusCode(201, result);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<GenericCommandResult>> ListAllBills([FromServices] IBillRepository repository)
        {
            var bills = await repository.GetAllBills();
            var result = new GenericCommandResult(true, "Bills fetched", bills);

            return StatusCode(200, result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<GenericCommandResult>> DeleteBill([FromServices] IBillRepository repository, Guid id)
        {
            await repository.DeleteBill(id);

            return StatusCode(200, new GenericCommandResult(true, "Bill deleted sucessfully", new { }));
        }
    }
}
