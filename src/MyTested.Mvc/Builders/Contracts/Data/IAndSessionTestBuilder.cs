namespace MyTested.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Http.ISession"/> tests.
    /// </summary>
    public interface IAndSessionTestBuilder : ISessionTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Http.ISession"/>.
        /// </summary>
        /// <returns>The same <see cref="ISessionTestBuilder"/>.</returns>
        ISessionTestBuilder AndAlso();
    }
}
