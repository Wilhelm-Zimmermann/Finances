namespace FinanceController.Domain.Api.Authentication;

public interface IPermissionService
{
    Task<bool> IsAuthorize(string userName, string privilege);
}
