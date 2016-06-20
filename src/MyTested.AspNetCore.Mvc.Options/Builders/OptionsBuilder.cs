namespace MyTested.AspNetCore.Mvc.Builders
{
    using System;
    using Base;
    using Contracts.Options;
    using Internal.TestContexts;
    using Utilities.Validators;

    /// <summary>
    /// Used for building configuration options.
    /// </summary>
    public class OptionsBuilder : BaseTestBuilder, IOptionsBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="HttpTestContext"/> containing data about the currently executed assertion chain.</param>
        public OptionsBuilder(HttpTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public void For<TOptions>(Action<TOptions> optionsSetup)
            where TOptions : class, new()
        {
            CommonValidator.CheckForNullReference(optionsSetup, nameof(optionsSetup));

            optionsSetup(this.TestContext.GetOptions<TOptions>());
        }
    }
}
