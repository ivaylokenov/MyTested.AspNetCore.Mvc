namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using Contracts.Base;
    using Internal.TestContexts;

    public abstract class BaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> : BaseTestBuilderWithComponent,
        IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder>
        where TBuilder : IBaseTestBuilder
    {
        private TBuilder builder;

        protected BaseTestBuilderWithComponentShouldHaveTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }

        public TBuilder Builder
        {
            get
            {
                if (this.builder == null)
                {
                    this.builder = this.SetBuilder();
                }

                return this.builder;
            }
        }

        protected abstract TBuilder SetBuilder();
    }
}
