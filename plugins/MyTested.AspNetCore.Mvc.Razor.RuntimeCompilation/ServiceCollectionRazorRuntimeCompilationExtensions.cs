namespace MyTested.AspNetCore.Mvc
{
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc.Internal.Razor;

    /// <summary>
    /// Contains Razor runtime compilation extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionRazorRuntimeCompilationExtensions
    {
        /// <summary>
        /// Replaces the default <see cref="IActionDescriptorChangeProvider"/> with a mocked implementation..
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceRazorRuntimeCompilation(this IServiceCollection serviceCollection)
            => serviceCollection
                .Remove<IActionDescriptorProvider, PageActionDescriptorProvider>()
                .ReplaceSingleton<IActionDescriptorChangeProvider, TestActionDescriptorChangeProvider>();
    }
}
