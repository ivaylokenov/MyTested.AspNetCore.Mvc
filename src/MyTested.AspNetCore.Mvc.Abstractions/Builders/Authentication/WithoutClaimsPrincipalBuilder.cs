namespace MyTested.AspNetCore.Mvc.Builders.Authentication
{
    using System.Security.Claims;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication;

    public class WithoutClaimsPrincipalBuilder : BaseClaimsPrincipalUserBuilder, IAndWithoutClaimsPrincipalBuilder
    {
        public IAndWithoutClaimsPrincipalBuilder WithoutClaim(string type, string value)
        {
            base.RemoveClaim(type, value);

            return this;
        }

        public IAndWithoutClaimsPrincipalBuilder WithoutClaim(Claim claim)
        {
            base.RemoveClaim(claim);

            return this;
        }

        public IAndWithoutClaimsPrincipalBuilder WithoutRole(string role)
        {
            base.RemoveRole(role);

            return this;
        }

        public IAndWithoutClaimsPrincipalBuilder WithoutUser()
        {
            return this;
        }

        public IAndWithoutClaimsPrincipalBuilder WithoutUsername(string username)
        {
            base.RemoveUsername(username);

            return this;
        }

        public IWithoutClaimsPrincipalBuilder AndAlso()
            => this;
    }
}
