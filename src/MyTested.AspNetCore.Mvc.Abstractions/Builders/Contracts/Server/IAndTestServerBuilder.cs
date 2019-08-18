namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Server
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="ITestServerBuilder"/> builder.
    /// </summary>
    public interface IAndTestServerBuilder : ITestServerBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building the test server.
        /// </summary>
        /// <returns>The same <see cref="ITestServerBuilder"/>.</returns>
        ITestServerBuilder AndAlso();
    }
}
