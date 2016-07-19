namespace MyTested.AspNetCore.Mvc.Builders.Base
{
    using Internal.TestContexts;

    public class BaseTestBuilderWithComponent<TBuilder> : BaseTestBuilder
    {
        public BaseTestBuilderWithComponent(HttpTestContext testContext)
            : base(testContext)
        {
        }

        public TBuilder Builder { get; protected set; }
    }
}
