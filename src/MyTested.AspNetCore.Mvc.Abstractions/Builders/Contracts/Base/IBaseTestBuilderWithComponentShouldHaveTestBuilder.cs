namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    /// <summary>
    /// Base interface for all test builders with component builders which have test assertions.
    /// </summary>
    /// <typeparam name="TBuilder">Base builder.</typeparam>
    public interface IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> : IBaseTestBuilder
        where TBuilder : IBaseTestBuilder
    {
    }
}
