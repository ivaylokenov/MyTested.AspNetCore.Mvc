namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http.Features.Authentication;

    public class RouteAuthenticationHandler : IAuthenticationHandler
    {
        public Task AuthenticateAsync(AuthenticateContext context)
        {
            return Task.CompletedTask;
        }

        public Task ChallengeAsync(ChallengeContext context)
        {
            context.Accept();
            return Task.CompletedTask;
        }

        public void GetDescriptions(DescribeSchemesContext context)
        { 
            // intentionally does nothing
        }

        public Task SignInAsync(SignInContext context)
        {
            context.Accept();
            return Task.CompletedTask;
        }

        public Task SignOutAsync(SignOutContext context)
        {
            context.Accept();
            return Task.CompletedTask;
        }
    }
}
