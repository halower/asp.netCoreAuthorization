using Microsoft.AspNetCore.Authorization;

namespace AuthorizationForoNetCore.Policy
{
    public class HasPasswordHandler : AuthorizationHandler<LoginRequirement>
    {
        protected override void Handle(AuthorizationContext context, LoginRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "UsernameAndPassword" && c.Issuer == "http://www.cnblogs.com/rohelm"))
                return;
            context.Succeed(requirement);
        }
    }
}
