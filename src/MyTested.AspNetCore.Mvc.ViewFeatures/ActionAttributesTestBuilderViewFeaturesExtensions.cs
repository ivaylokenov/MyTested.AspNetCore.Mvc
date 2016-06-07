namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Attributes;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains view features extension methods for <see cref="IActionAttributesTestBuilder"/>.
    /// </summary>
    public static class ActionAttributesTestBuilderViewFeaturesExtensions
    {
        /// <summary>
        /// Tests whether the action attributes contain <see cref="ValidateAntiForgeryTokenAttribute"/>.
        /// </summary>
        /// <param name="actionAttributesTestBuilder">Instance of <see cref="IActionAttributesTestBuilder"/> type.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        public static IAndActionAttributesTestBuilder ValidatingAntiForgeryToken(this IActionAttributesTestBuilder actionAttributesTestBuilder)
        {
            return actionAttributesTestBuilder.ContainingAttributeOfType<ValidateAntiForgeryTokenAttribute>();
        }
    }
}
