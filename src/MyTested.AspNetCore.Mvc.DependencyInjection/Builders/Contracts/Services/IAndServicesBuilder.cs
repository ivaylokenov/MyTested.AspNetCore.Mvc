namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Services
{
    /// <summary>
    /// Used for adding AndAlso() method to the services builder.
    /// </summary>
    public interface IAndServicesBuilder : IServicesBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when setting services.
        /// </summary>
        /// <returns>The same <see cref="IServicesBuilder"/>.</returns>
        IServicesBuilder AndAlso();
    }
}
