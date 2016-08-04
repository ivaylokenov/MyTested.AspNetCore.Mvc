namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    public interface IBaseTestBuilderWithComponentBuilder<TBuilder> : IBaseTestBuilder
        where TBuilder : IBaseTestBuilder
    {
    }
}
