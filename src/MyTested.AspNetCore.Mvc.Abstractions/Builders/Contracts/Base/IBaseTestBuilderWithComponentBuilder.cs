namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    public interface IBaseTestBuilderWithComponentBuilder<TBuilder> : IBaseTestBuilderWithComponent
        where TBuilder : IBaseTestBuilder
    {
    }
}
