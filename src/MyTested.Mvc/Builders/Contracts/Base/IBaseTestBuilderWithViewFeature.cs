namespace MyTested.Mvc.Builders.Contracts.Base
{
    using Models;

    public interface IBaseTestBuilderWithViewFeature : IBaseTestBuilderWithCaughtException
    {
        IModelDetailsTestBuilder<TModel> WithModel<TModel>(TModel model);

        IModelDetailsTestBuilder<TModel> WithModelOfType<TModel>();
    }
}
