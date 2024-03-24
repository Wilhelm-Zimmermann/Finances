using FinanceController.Domain.Api.Utils;
using FinanceController.Domain.Commands;
using FinanceController.Domain.Entities;
using FinanceController.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceController.Domain.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<GenericCommandResult>> CreateUser([FromBody] CreateUserCommand command, [FromServices] UserHandler handler)
        {
            var user = await handler.Handle(command);

            return StatusCode(201, user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<GenericCommandResult>> Login([FromBody] LoginUserCommand command, [FromServices] UserHandler handler)
        {
            var result = (GenericCommandResult) await handler.Handle(command);
            string token;

            if(result.Success)
            {
                token = JwtTokenService.GenerateToken((User)result.Data);
                var userRole = JwtTokenService.GetUserRole(token);

                return StatusCode(200, new GenericCommandResult(true, "User logged successfully", new
                {
                    Token = token,
                }));
            }

            return StatusCode(200, new GenericCommandResult(true, "ok", result));
        }
    }
}
