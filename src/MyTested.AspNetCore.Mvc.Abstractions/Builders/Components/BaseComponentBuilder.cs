namespace MyTested.AspNetCore.Mvc.Builders.Components
{
    using Base;
    using Contracts.Base;
    using Internal.TestContexts;

    public abstract class BaseComponentBuilder<TBuilder> : BaseTestBuilderWithComponentBuilder<TBuilder>
        where TBuilder : IBaseTestBuilder
    {
        public BaseComponentBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
            this.TestContext.ComponentBuildDelegate += this.BuildComponentIfNotExists;
        }

        protected abstract void BuildComponentIfNotExists();
    }
}
