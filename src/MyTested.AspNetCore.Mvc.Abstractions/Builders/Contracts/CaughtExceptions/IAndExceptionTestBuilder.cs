namespace MyTested.AspNetCore.Mvc.Builders.Contracts.CaughtExceptions
{
    /// <summary>
    /// Used for adding AndAlso() method to the expected <see cref="System.Exception"/> tests.
    /// </summary>
    public interface IAndExceptionTestBuilder : IExceptionTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining expected <see cref="System.Exception"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IExceptionTestBuilder"/>.</returns>
        IExceptionTestBuilder AndAlso();
    }
}
