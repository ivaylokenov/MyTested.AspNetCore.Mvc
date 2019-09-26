namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Used for building <see cref="Microsoft.AspNetCore.Http.ISession"/>.
    /// </summary>
    public interface IWithoutSessionBuilder
    {
        IAndWithoutSessionBuilder WithoutEntry(string key);

        IAndWithoutSessionBuilder ClearSession();
    }
}
