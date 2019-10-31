namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication
{
    using System.Collections.Generic;
    using System.Security.Claims;

    public interface IWithoutClaimsIdentityBuilder
    {
        IAndWithoutClaimsIdentityBuilder WithoutNameType(string nameType);

        IAndWithoutClaimsIdentityBuilder WithoutRoleType(string roleType);

        IAndWithoutClaimsIdentityBuilder WithoutIdentifier(string identifier);

        IAndWithoutClaimsIdentityBuilder WithoutUsername(string username);

        IAndWithoutClaimsIdentityBuilder WithoutClaim(string type, string value);

        IAndWithoutClaimsIdentityBuilder WithoutClaim(Claim claim);

        IAndWithoutClaimsIdentityBuilder WithoutClaims(IEnumerable<Claim> claims);

        IAndWithoutClaimsIdentityBuilder WithoutClaims(params Claim[] claims);
    }
}
