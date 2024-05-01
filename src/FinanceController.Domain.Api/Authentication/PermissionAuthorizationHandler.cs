using FinanceController.Domain.Api.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace FinanceController.Domain.Infra.Authentication;
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _scope;

    public PermissionAuthorizationHandler(IServiceScopeFactory scope)
    {
        _scope = scope;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var user = context.User.Claims.ToList();
        var userName = context.User.Claims.FirstOrDefault(x => x.Type == "username")?.Value;

        using var scope = _scope.CreateScope();
        var permissionContext = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        var hasPermission = await permissionContext.IsAuthorize(userName, requirement.Privilege);

        if(hasPermission)
        {
            context.Succeed(requirement);
        }
    }
}
