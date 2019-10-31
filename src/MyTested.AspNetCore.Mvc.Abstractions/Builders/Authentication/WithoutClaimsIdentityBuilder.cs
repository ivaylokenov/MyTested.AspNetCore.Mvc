namespace MyTested.AspNetCore.Mvc.Builders.Authentication
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication;

    public class WithoutClaimsIdentityBuilder : IAndWithoutClaimsIdentityBuilder
    { 
        public IAndWithoutClaimsIdentityBuilder WithoutClaim(string type, string value)
        {
            throw new System.NotImplementedException();
        }

        public IAndWithoutClaimsIdentityBuilder WithoutClaim(Claim claim)
        {
            throw new System.NotImplementedException();
        }

        public IAndWithoutClaimsIdentityBuilder WithoutClaims(IEnumerable<Claim> claims)
        {
            throw new System.NotImplementedException();
        }

        public IAndWithoutClaimsIdentityBuilder WithoutClaims(params Claim[] claims)
        {
            throw new System.NotImplementedException();
        }

        public IAndWithoutClaimsIdentityBuilder WithoutIdentifier(string identifier)
        {
            throw new System.NotImplementedException();
        }

        public IAndWithoutClaimsIdentityBuilder WithoutNameType(string nameType)
        {
            throw new System.NotImplementedException();
        }

        public IAndWithoutClaimsIdentityBuilder WithoutRoleType(string roleType)
        {
            throw new System.NotImplementedException();
        }

        public IAndWithoutClaimsIdentityBuilder WithoutUsername(string username)
        {
            throw new System.NotImplementedException();
        }

        public IWithoutClaimsIdentityBuilder AndAlso()
            => this;
    }
}
