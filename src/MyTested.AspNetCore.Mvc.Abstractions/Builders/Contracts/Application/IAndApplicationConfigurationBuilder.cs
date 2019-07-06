namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Application
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="IApplicationConfigurationBuilder"/> builder.
    /// </summary>
    public interface IAndApplicationConfigurationBuilder : IApplicationConfigurationBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building the test application.
        /// </summary>
        /// <returns>The same <see cref="IApplicationConfigurationBuilder"/>.</returns>
        IApplicationConfigurationBuilder AndAlso();
    }
}
