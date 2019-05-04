namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.View;
    using Microsoft.AspNetCore.Mvc.ViewEngines;

    /// <summary>
    /// Contains extension methods for <see cref="IViewTestBuilder" />.
    /// </summary>
    public static class ViewTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/>
        /// has the same <see cref="IViewEngine"/> type as the provided one.
        /// </summary>
        /// <param name="viewTestBuilder">
        /// Instance of <see cref="IViewTestBuilder"/> type.
        /// </param>
        /// <typeparam name="TViewEngine">View engine of type <see cref="IViewEngine"/>.</typeparam>
        /// <returns>The same <see cref="IAndViewTestBuilder"/>.</returns>
        public static IAndViewTestBuilder WithViewEngineOfType<TViewEngine>(
            this IViewTestBuilder viewTestBuilder)
            where TViewEngine : IViewEngine
            => viewTestBuilder
                .WithViewEngineOfType<IAndViewTestBuilder, TViewEngine>();
    }
}
