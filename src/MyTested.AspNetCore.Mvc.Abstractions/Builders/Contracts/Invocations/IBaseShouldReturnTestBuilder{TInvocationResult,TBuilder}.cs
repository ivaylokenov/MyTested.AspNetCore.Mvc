namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Invocations
{
    public interface IBaseShouldReturnTestBuilder<TInvocationResult, TBuilder>
        : IBaseShouldReturnTestBuilder<TInvocationResult>
    {
        /// <summary>
        /// Tests whether the result is the default value of the type.
        /// </summary>
        /// <returns>Base test builder.</returns>
        TBuilder DefaultValue();

        /// <summary>
        /// Tests whether the result is null.
        /// </summary>
        /// <returns>Base test builder.</returns>
        TBuilder Null();

        /// <summary>
        /// Tests whether the result is not null.
        /// </summary>
        /// <returns>Base test builder.</returns>
        TBuilder NotNull();
    }
}
