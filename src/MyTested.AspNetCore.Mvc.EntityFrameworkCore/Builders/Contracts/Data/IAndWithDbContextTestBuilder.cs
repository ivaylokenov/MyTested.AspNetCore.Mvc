namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> test builder.
    /// </summary>
    public interface IAndWithDbContextTestBuilder : IWithDbContextTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.
        /// </summary>
        /// <returns>The same <see cref="IWithDbContextTestBuilder"/>.</returns>
        IWithDbContextTestBuilder AndAlso();
    }
}
