namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using Contracts.Base;
    using Internal.TestContexts;

    public abstract class BaseTestBuilderWithComponentBuilder<TBuilder> : BaseTestBuilderWithComponent,
        IBaseTestBuilderWithComponentBuilder<TBuilder>
        where TBuilder : IBaseTestBuilder
    {
        private TBuilder builder;

        protected BaseTestBuilderWithComponentBuilder(ComponentTestContext testContext)
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
