namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Attributes;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains view features extension methods for <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/>.
    /// </summary>
    public static class ControllerActionAttributesTestBuilderViewFeaturesExtensions
    {
        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ValidateAntiForgeryTokenAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.</param>
        /// <typeparam name="TAttributesTestBuilder">Type of attributes test builder to use as a return type.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder ValidatingAntiForgeryToken<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder)
            where TAttributesTestBuilder : IBaseAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder.ContainingAttributeOfType<ValidateAntiForgeryTokenAttribute>();
        
        /// <summary>
        /// Tests whether the collected attributes contain <see cref="IgnoreAntiforgeryTokenAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.</param>
        /// <typeparam name="TAttributesTestBuilder">Type of attributes test builder to use as a return type.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder IgnoringAntiForgeryToken<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder)
            where TAttributesTestBuilder : IBaseAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder.ContainingAttributeOfType<IgnoreAntiforgeryTokenAttribute>();

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="SkipStatusCodePagesAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.</param>
        /// <typeparam name="TAttributesTestBuilder">Type of attributes test builder to use as a return type.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SkippingStatusCodePages<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder)
            where TAttributesTestBuilder : IBaseAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder.ContainingAttributeOfType<SkipStatusCodePagesAttribute>();
    }
}
