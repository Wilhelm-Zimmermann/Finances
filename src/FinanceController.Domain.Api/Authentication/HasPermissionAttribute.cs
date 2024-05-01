using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FinanceController.Domain.Api.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public string Privilege { get; set; }

    public HasPermissionAttribute(string privilege) : base(policy: privilege)
    {
        Privilege = privilege;
    }
}
