namespace MyTested.Mvc.Builders.Contracts.ActionResults.Ok
{
    /// <summary>
    /// Used for adding AndAlso() method to the the OK response tests.
    /// </summary>
    public interface IAndOkTestBuilder : IOkTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining OK tests.
        /// </summary>
        /// <returns>The same OK test builder.</returns>
        IOkTestBuilder AndAlso();
    }
}
