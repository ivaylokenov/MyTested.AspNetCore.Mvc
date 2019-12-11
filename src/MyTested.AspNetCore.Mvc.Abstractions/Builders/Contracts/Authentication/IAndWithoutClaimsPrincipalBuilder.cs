namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication
{
    public interface IAndWithoutClaimsPrincipalBuilder : IWithoutClaimsPrincipalBuilder
    {
        IWithoutClaimsPrincipalBuilder AndAlso();
    }
}
