using FinanceController.Domain.Api.Authentication;
using FinanceController.Domain.Commands;
using FinanceController.Domain.Handlers;
using FinanceController.Domain.Infra.Commons.Constants;
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
        [HasPermission(Privileges.BillCreate)]
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
        [HasPermission(Privileges.BillRead)]
        public async Task<ActionResult<GenericCommandResult>> ListAllBills([FromServices] IBillRepository repository)
        {
            var bills = await repository.GetAllBills();
            var result = new GenericCommandResult(true, "Bills fetched", bills);

            return StatusCode(200, result);
        }

        [HttpGet]
        [Route("list/user-logged")]
        [HasPermission(Privileges.BillRead)]
        public async Task<ActionResult<GenericCommandResult>> ListLoggedUserBills([FromServices] IBillRepository repository)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId") ?? throw new NullReferenceException();
            var bills = await repository.ListBillsByUserId(Guid.Parse(userId.Value));

            if(bills.ToArray().Length <= 0)
            {
                return NotFound("No bills for this user");
            }

            return Ok(new GenericCommandResult(true, "Bills fetched successfully", bills));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [HasPermission(Privileges.BillDelete)]
        public async Task<ActionResult<GenericCommandResult>> DeleteBill([FromServices] IBillRepository repository, Guid id)
        {
            await repository.DeleteBill(id);

            return StatusCode(200, new GenericCommandResult(true, "Bill deleted sucessfully", new { }));
        }
    }
}
