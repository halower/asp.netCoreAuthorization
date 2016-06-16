using AuthorizationForoNetCore.Modles;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace AuthorizationForoNetCore.Policy
{
    public class DocumentEditHandler : AuthorizationHandler<EditRequirement, Document>
    {
        protected override void Handle(AuthorizationContext context, EditRequirement requirement, Document resource)
        {
            if (resource.Author == context.User.FindFirst(ClaimTypes.Name).Value)
            {
                context.Succeed(requirement);
            }
        }
    }
}
