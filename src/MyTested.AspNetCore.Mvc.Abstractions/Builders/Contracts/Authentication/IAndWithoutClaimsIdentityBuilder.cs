namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication
{
    public interface IAndWithoutClaimsIdentityBuilder : IWithoutClaimsIdentityBuilder
    {
        IWithoutClaimsIdentityBuilder AndAlso();
    }
}
