using Microsoft.AspNetCore.Authorization;
using System;

namespace AuthorizationForoNetCore.Policy
{
    public class HasAccessTokenHandler : AuthorizationHandler<LoginRequirement>
    {
        protected override void Handle(AuthorizationContext context, LoginRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "AccessToken" && c.Issuer == "http://www.cnblogs.com/rohelm"))
                return;

            var toeknExpiryIn = Convert.ToDateTime(context.User.FindFirst(c => c.Type == "AccessToken" && c.Issuer == "http://www.cnblogs.com/rohelm").Value);

            if (toeknExpiryIn > DateTime.Now)
            {
                context.Succeed(requirement);
            }
        }
    }
}
