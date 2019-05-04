namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.View;
    using Microsoft.AspNetCore.Mvc.ViewEngines;

    /// <summary>
    /// Contains extension methods for <see cref="IPartialViewTestBuilder" />.
    /// </summary>
    public static class PartialViewTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/>
        /// has the same <see cref="IViewEngine"/> type as the provided one.
        /// </summary>
        /// <param name="viewTestBuilder">
        /// Instance of <see cref="IPartialViewTestBuilder"/> type.
        /// </param>
        /// <typeparam name="TViewEngine">View engine of type <see cref="IViewEngine"/>.</typeparam>
        /// <returns>The same <see cref="IAndPartialViewTestBuilder"/>.</returns>
        public static IAndPartialViewTestBuilder WithViewEngineOfType<TViewEngine>(
            this IPartialViewTestBuilder viewTestBuilder)
            where TViewEngine : IViewEngine
            => viewTestBuilder
                .WithViewEngineOfType<IAndPartialViewTestBuilder, TViewEngine>();
    }
}
