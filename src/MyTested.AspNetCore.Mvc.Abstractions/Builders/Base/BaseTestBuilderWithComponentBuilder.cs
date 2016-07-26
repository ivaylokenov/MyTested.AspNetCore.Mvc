namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using Contracts.Base;
    using Internal.TestContexts;

    public abstract class BaseTestBuilderWithComponentBuilder<TBuilder> : BaseTestBuilder,
        IBaseTestBuilderWithComponentBuilder<TBuilder>
        where TBuilder : IBaseTestBuilder
    {
        private TBuilder builder;

        protected BaseTestBuilderWithComponentBuilder(HttpTestContext testContext)
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

                return builder;
            }
        }

        protected abstract TBuilder SetBuilder();
    }
}
