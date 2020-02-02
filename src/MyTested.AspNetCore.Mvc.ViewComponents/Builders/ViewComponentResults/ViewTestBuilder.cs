namespace MyTested.AspNetCore.Mvc.Builders.ViewComponentResults
{
    using System;
    using And;
    using Base;
    using Contracts.And;
    using Contracts.ViewComponentResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Exceptions;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing <see cref="ViewViewComponentResult"/>.
    /// </summary>
    public class ViewTestBuilder : BaseTestBuilderWithResponseModel, IAndViewTestBuilder
    {
        public ViewTestBuilder(ActionTestContext testContext)
            : base(testContext) 
            => this.ViewResult = testContext.MethodResultAs<ViewViewComponentResult>();
        
        public ViewViewComponentResult ViewResult { get; private set; }
        
        /// <inheritdoc />
        public IViewTestBuilder AndAlso() => this;

        public override object GetActualModel()
            => this.TestContext.MethodResultAs<ViewViewComponentResult>()?.ViewData?.Model;

        public override Type GetModelReturnType() => this.GetActualModel()?.GetType();

        public override void ValidateNoModel()
        {
            if (this.GetActualModel() != null)
            {
                throw new ResponseModelAssertionException(string.Format(
                    "{0} to not have a view model but in fact such was found.",
                    this.TestContext.ExceptionMessagePrefix));
            }
        }

        public IAndTestBuilder Passing(Action<ViewViewComponentResult> assertions)
        {
            assertions(this.ViewResult);

            return new AndTestBuilder(this.TestContext);
        }

        public IAndTestBuilder Passing(Func<ViewViewComponentResult, bool> predicate)
        {
            if (!predicate(this.ViewResult))
            {
                throw new InvocationResultAssertionException(string.Format(
                    "{0} the {1} to pass the given predicate, but it failed.",
                    this.TestContext.ExceptionMessagePrefix,
                    this.TestContext.MethodResult.GetName()));
            }

            return new AndTestBuilder(this.TestContext);
        }
    }
}
