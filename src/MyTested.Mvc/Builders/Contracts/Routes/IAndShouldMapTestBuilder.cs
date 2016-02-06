namespace MyTested.Mvc.Builders.Contracts.Routes
{
    /// <summary>
    /// Used for adding And() method to the route request builder.
    /// </summary>
    public interface IAndShouldMapTestBuilder : IShouldMapTestBuilder
    {
        /// <summary>
        /// And method for better readability when building route HTTP request.
        /// </summary>
        /// <returns>The same should map test builder.</returns>
        IShouldMapTestBuilder And();
    }
}
