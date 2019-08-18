namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using System;
    using And;
    using Contracts.And;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;
    using Utilities.Validators;

    public abstract class BaseTestBuilderWithActionContext : BaseTestBuilderWithComponent
    {
        private ActionTestContext testContext;

        protected BaseTestBuilderWithActionContext(ActionTestContext testContext) 
            : base(testContext) 
            => this.TestContext = testContext;

        /// <summary>
        /// Gets the currently used <see cref="ActionTestContext"/>.
        /// </summary>
        /// <value>Result of type <see cref="ActionTestContext"/>.</value>
        public new ActionTestContext TestContext
        {
            get => this.testContext;

            private set
            {
                CommonValidator.CheckForNullReference(value, nameof(this.TestContext));
                this.testContext = value;
            }
        }
        
        protected IAndTestBuilder Passing<TActionResult>(Action<TActionResult> assertions)
        {
            assertions(this.TestContext.MethodResultAs<TActionResult>());

            return new AndTestBuilder(this.TestContext);
        }

        protected IAndTestBuilder Passing<TActionResult>(Func<TActionResult, bool> predicate)
        {
            if (!predicate(this.TestContext.MethodResultAs<TActionResult>()))
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
