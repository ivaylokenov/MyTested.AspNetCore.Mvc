namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using Internal.TestContexts;

    public class BaseTestBuilderWithComponentBuilder<TBuilder> : BaseTestBuilder
    {
        public BaseTestBuilderWithComponentBuilder(HttpTestContext testContext)
            : base(testContext)
        {
        }

        public TBuilder Builder { get; protected set; }
    }
}
