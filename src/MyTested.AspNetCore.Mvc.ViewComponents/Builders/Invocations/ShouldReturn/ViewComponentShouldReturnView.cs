namespace MyTested.AspNetCore.Mvc.Builders.Invocations.ShouldReturn
{
    using System;
    using Builders.And;
    using Contracts.And;
    using Contracts.ViewComponentResults;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using ViewComponentResults;
    using Utilities.Validators;

    public partial class ViewComponentShouldReturnTestBuilder<TInvocationResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder View() => this.View(null);

        /// <inheritdoc />
        public IAndTestBuilder View(Action<IViewTestBuilder> viewTestBuilder)
        {
            InvocationResultValidator.ValidateInvocationResultType<ViewViewComponentResult>(this.TestContext);

            viewTestBuilder?.Invoke(new ViewTestBuilder(this.TestContext));

            return new AndTestBuilder(this.TestContext);
        }
    }
}
