namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Dependencies
{
    /// <summary>
    /// Used for adding AndAlso() method to the service dependencies builder.
    /// </summary>
    public interface IAndDependenciesBuilder : IDependenciesBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when setting service dependencies.
        /// </summary>
        /// <returns>The same <see cref="IDependenciesBuilder"/>.</returns>
        IDependenciesBuilder AndAlso();
    }
}
