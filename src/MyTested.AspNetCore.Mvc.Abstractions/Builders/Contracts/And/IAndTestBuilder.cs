namespace MyTested.AspNetCore.Mvc.Builders.Contracts.And
{
    using Base;

    /// <summary>
    /// Used for adding 'AndAlso' method to <see cref="IBaseTestBuilderWithComponent"/>.
    /// </summary>
    public interface IAndTestBuilder : IBaseTestBuilderWithComponent
    {
        /// <summary>
        /// AndAlso method for better readability when chaining should pass for tests.
        /// </summary>
        /// <returns>The same <see cref="IBaseTestBuilderWithComponent"/>.</returns>
        IBaseTestBuilderWithComponent AndAlso();
    }
}
