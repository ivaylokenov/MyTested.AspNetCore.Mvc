namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ShouldPassFor
{
    /// <summary>
    /// Test builder allowing additional assertions on various components.
    /// </summary>
    /// <typeparam name="TComponent">Class representing ASP.NET Core MVC component.</typeparam>
    public interface IShouldPassForTestBuilderWithComponent<TComponent> : IShouldPassForTestBuilder
        where TComponent : class
    {
    }
}
