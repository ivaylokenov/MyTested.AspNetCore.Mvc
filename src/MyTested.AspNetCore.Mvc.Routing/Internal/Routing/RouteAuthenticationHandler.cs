namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http.Features.Authentication;
    using Microsoft.AspNetCore.Mvc.Internal;

    public class RouteAuthenticationHandler : IAuthenticationHandler
    {
        public Task AuthenticateAsync(AuthenticateContext context)
        {
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
