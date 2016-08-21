namespace MyTested.AspNetCore.Mvc.Builders.And
{
    using Base;
    using Contracts.And;
    using Contracts.Base;
    using Internal.TestContexts;

    /// <summary>
    /// AndAlso method for better readability when chaining should pass for tests.
    /// </summary>
    public class AndTestBuilder : BaseTestBuilderWithComponent, IAndTestBuilder
    {
        public AndTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IBaseTestBuilderWithComponent AndAlso() => this;
    }
}
