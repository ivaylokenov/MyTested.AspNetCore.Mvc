namespace MyTested.Mvc.Builders.Contracts.ActionResults.View
{
    using Base;
    using Models;

    public interface IViewTestBuilder : IBaseTestBuilderWithCaughtException
    {
        IModelDetailsTestBuilder<TModel> WithModel<TModel>(TModel model);

        IModelDetailsTestBuilder<TModel> WithModelOfType<TModel>();
    }
}
