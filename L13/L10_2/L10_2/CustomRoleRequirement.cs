using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L10_2
{    public class CustomRoleRequirement : AuthorizationHandler<CustomRoleRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRoleRequirement requirement)
        {
            var roles = new[] { "Admin" };
            var userIsInRole = roles.Any(role => context.User.IsInRole(role));
            if (userIsInRole)
            {
                context.Fail();
                return Task.FromResult(false);
            }

            context.Succeed(requirement);
            return Task.FromResult(true);
        }
    }
}
