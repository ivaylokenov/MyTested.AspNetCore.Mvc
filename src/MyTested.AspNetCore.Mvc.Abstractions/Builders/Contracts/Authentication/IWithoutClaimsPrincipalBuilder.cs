namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication
{
    using System.Security.Claims;

    public interface IWithoutClaimsPrincipalBuilder
    {
        IAndWithoutClaimsPrincipalBuilder WithoutUser();

        IAndWithoutClaimsPrincipalBuilder WithoutRole(string role);

        IAndWithoutClaimsPrincipalBuilder WithoutUsername(string username);

        IAndWithoutClaimsPrincipalBuilder WithoutClaim(string type, string value);

        IAndWithoutClaimsPrincipalBuilder WithoutClaim(Claim claim);
    }
}
