namespace MyTested.AspNetCore.Mvc.Builders
{
    using System;
    using Base;
    using Contracts;
    using Internal.TestContexts;
    using Utilities.Validators;

    public class OptionsBuilder : BaseTestBuilder, IOptionsBuilder
    {
        public OptionsBuilder(HttpTestContext testContext)
            : base(testContext)
        {
        }

        public void For<TOptions>(Action<TOptions> optionsSetup)
            where TOptions : class, new()
        {
            CommonValidator.CheckForNullReference(optionsSetup, nameof(optionsSetup));

            optionsSetup(this.TestContext.GetOptions<TOptions>());
        }
    }
}
