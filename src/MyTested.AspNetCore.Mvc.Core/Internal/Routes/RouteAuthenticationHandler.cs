namespace MyTested.AspNetCore.Mvc.Internal.Routes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Builders.Authentication;
    using Microsoft.AspNetCore.Http.Features.Authentication;
    using Microsoft.AspNetCore.Mvc.Internal;

    public class RouteAuthenticationHandler : IAuthenticationHandler
    {
        public Task AuthenticateAsync(AuthenticateContext context)
        {
            context.Authenticated(
                ClaimsPrincipalBuilder.DefaultAuthenticated,
                new Dictionary<string, string>(),
                new Dictionary<string, object>());

            return TaskCache.CompletedTask;
        }

        public Task ChallengeAsync(ChallengeContext context)
        {
            context.Accept();
            return TaskCache.CompletedTask;
        }

        public void GetDescriptions(DescribeSchemesContext context)
        { 
            // intentionally does nothing
        }

        public Task SignInAsync(SignInContext context)
        {
            context.Accept();
            return TaskCache.CompletedTask;
        }

        public Task SignOutAsync(SignOutContext context)
        {
            context.Accept();
            return TaskCache.CompletedTask;
        }
    }
}
