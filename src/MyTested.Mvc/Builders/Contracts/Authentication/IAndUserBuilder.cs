namespace MyTested.Mvc.Builders.Contracts.Authentication
{
    /// <summary>
    /// Used for adding AndAlso() method to the user builder.
    /// </summary>
    public interface IAndUserBuilder : IUserBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building user.
        /// </summary>
        /// <returns>The same user builder.</returns>
        IUserBuilder AndAlso();
    }
}
