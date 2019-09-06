namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Routing;

    /// <summary>
    /// Provides methods to specify an ASP.NET Core MVC pipeline test case.
    /// </summary>
    public class MyPipeline : MyRouting
    {
        /// <summary>
        /// Starts a pipeline test.
        /// </summary>
        /// <returns>Test builder of <see cref="IRouteTestBuilder"/> type.</returns>
        public new static IRouteTestBuilder Configuration() => new MyPipeline();
    }
}
