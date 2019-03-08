namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for testing controller attributes.
    /// </summary>
    public interface IControllerAttributesTestBuilder : IControllerActionAttributesTestBuilder<IAndControllerAttributesTestBuilder>
    {
        /// <summary>
        /// Tests whether the controller attributes contain <see cref="Microsoft.AspNetCore.Mvc.ControllerAttribute"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndControllerAttributesTestBuilder"/>.</returns>
        IAndControllerAttributesTestBuilder IndicatingControllerExplicitly();

        /// <summary>
        /// Tests whether the controller attributes contain <see cref="Microsoft.AspNetCore.Mvc.ApiControllerAttribute"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndControllerAttributesTestBuilder"/>.</returns>
        IAndControllerAttributesTestBuilder IndicatingApiController();
    }
}
