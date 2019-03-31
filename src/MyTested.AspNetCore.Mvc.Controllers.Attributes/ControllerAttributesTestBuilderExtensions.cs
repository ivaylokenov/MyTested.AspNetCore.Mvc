namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Attributes;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains extension methods for <see cref="IControllerAttributesTestBuilder"/>.
    /// </summary>
    public static class ControllerAttributesTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the controller attributes contain <see cref="ControllerAttribute"/>.
        /// </summary>
        /// <param name="controllerAttributesTestBuilder">
        /// Instance of <see cref="IControllerAttributesTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndControllerAttributesTestBuilder"/>.</returns>
        public static IAndControllerAttributesTestBuilder IndicatingControllerExplicitly(
            this IControllerAttributesTestBuilder controllerAttributesTestBuilder)
            => controllerAttributesTestBuilder
                .ContainingAttributeOfType<ControllerAttribute>();

        /// <summary>
        /// Tests whether the controller attributes contain <see cref="ApiControllerAttribute"/>.
        /// </summary>
        /// <param name="controllerAttributesTestBuilder">
        /// Instance of <see cref="IControllerAttributesTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndControllerAttributesTestBuilder"/>.</returns>
        public static IAndControllerAttributesTestBuilder IndicatingApiController(
            this IControllerAttributesTestBuilder controllerAttributesTestBuilder)
            => controllerAttributesTestBuilder
                .ContainingAttributeOfType<ApiControllerAttribute>();
    }
}
