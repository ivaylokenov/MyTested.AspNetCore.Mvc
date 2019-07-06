namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    /// <summary>
    /// Base interface for all test builders with component builders.
    /// </summary>
    /// <typeparam name="TBuilder">Base builder.</typeparam>
    public interface IBaseTestBuilderWithComponentBuilder<TBuilder> : IBaseTestBuilderWithComponent
        where TBuilder : IBaseTestBuilder
    {
    }
}
