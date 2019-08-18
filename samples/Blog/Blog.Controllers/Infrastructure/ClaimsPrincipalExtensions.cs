namespace Blog.Controllers.Infrastructure
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        
        public static bool IsAdministrator(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.IsInRole(ControllerConstants.AdministratorRole);
    }
}
