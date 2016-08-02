namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> test builder.
    /// </summary>
    public interface IAndDbContextTestBuilder : IDbContextTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.
        /// </summary>
        /// <returns>The same <see cref="IDbContextTestBuilder"/>.</returns>
        IDbContextTestBuilder AndAlso();
    }
}
