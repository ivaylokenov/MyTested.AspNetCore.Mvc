namespace MyTested.AspNetCore.Mvc
{
    using Builders.Attributes;
    using Builders.Contracts.Attributes;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;

    /// <summary>
    /// Contains extension methods for <see cref="IViewComponentAttributesTestBuilder"/>.
    /// </summary>
    public static class ViewComponentAttributesTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the controller attributes contain <see cref="ViewComponentAttribute"/>.
        /// </summary>
        /// <param name="viewComponentAttributesTestBuilder">
        /// Instance of <see cref="IViewComponentAttributesTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndViewComponentAttributesTestBuilder"/>.</returns>
        public static IAndViewComponentAttributesTestBuilder IndicatingViewComponentExplicitly(
            this IViewComponentAttributesTestBuilder viewComponentAttributesTestBuilder)
            => viewComponentAttributesTestBuilder
                .ContainingAttributeOfType<ViewComponentAttribute>();

        /// <summary>
        /// Tests whether the view component attributes contain <see cref="ViewComponentAttribute"/>.
        /// </summary>
        /// <param name="viewComponentAttributesTestBuilder">
        /// Instance of <see cref="IViewComponentAttributesTestBuilder"/> type.
        /// </param>
        /// <param name="viewComponentName">Expected overridden name of the view component.</param>
        /// <returns>The same <see cref="IAndViewComponentAttributesTestBuilder"/>.</returns>
        public static IAndViewComponentAttributesTestBuilder ChangingViewComponentNameTo(
            this IViewComponentAttributesTestBuilder viewComponentAttributesTestBuilder,
            string viewComponentName)
        {
            var actualBuilder = (BaseAttributesTestBuilder<IAndViewComponentAttributesTestBuilder>)viewComponentAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<ViewComponentAttribute>();

            actualBuilder.Validations.Add(attrs =>
            {
                var viewComponentAttribute = actualBuilder.GetAttributeOfType<ViewComponentAttribute>(attrs);
                var actualViewComponentName = viewComponentAttribute.Name;
                if (viewComponentName != actualViewComponentName)
                {
                    actualBuilder.ThrowNewAttributeAssertionException(
                        $"{viewComponentAttribute.GetName()} with '{viewComponentName}' name",
                        $"in fact found '{actualViewComponentName}'");
                }
            });

            return actualBuilder.AttributesTestBuilder;
        }
    }
}
