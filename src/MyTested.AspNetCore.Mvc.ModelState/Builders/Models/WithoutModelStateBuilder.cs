namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Models;
    using MyTested.AspNetCore.Mvc.Internal.TestContexts;

    /// <summary>
    /// Used for building <see cref="ModelStateDictionary"/>.
    /// </summary>
    public class WithoutModelStateBuilder : BaseModelStateBuilder, IAndWithoutModelStateBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithoutModelStateBuilder"/> class.
        /// </summary>
        /// <param name="actionContext"><see cref="ModelStateDictionary"/> to build.</param>
        public WithoutModelStateBuilder(ActionTestContext actionContext) 
            : base(actionContext)
        {
        }

        /// <inheritdoc />
        public IAndWithoutModelStateBuilder WithoutModelState()
        {
            this.ModelState.Clear();
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutModelStateBuilder WithoutModelState(string key)
        {
            if (this.ModelState.ContainsKey(key))
            {
                this.ModelState.Remove(key);
            }

            return this;
        }

        /// <inheritdoc />
        public IWithoutModelStateBuilder AndAlso()
            => this;
    }
}
