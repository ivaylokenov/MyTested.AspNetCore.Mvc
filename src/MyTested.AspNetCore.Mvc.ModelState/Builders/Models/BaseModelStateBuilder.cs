namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using MyTested.AspNetCore.Mvc.Internal.TestContexts;

    /// <summary>
    /// Used for testing specific <see cref="ModelStateDictionary"/>.
    /// </summary>
    public abstract class BaseModelStateBuilder
    {
        /// <summary>
        /// Abstract <see cref="BaseModelStateBuilder"/> class.
        /// </summary>
        /// <param name="actionContext"><see cref="ModelStateDictionary"/> to build.</param>
        public BaseModelStateBuilder(ActionTestContext actionContext)
            => this.ModelState = actionContext.ModelState;

        /// <summary>
        /// Gets the <see cref="ModelStateDictionary"/>
        /// </summary>
        /// <value>The built <see cref="ModelStateDictionary"/></value>
        protected ModelStateDictionary ModelState { get; set; }
    }
}
