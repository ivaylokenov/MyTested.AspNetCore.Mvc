namespace MyTested.Mvc.Builders.Contracts.ShouldPassFor
{
    using System;

    public interface IShouldPassForTestBuilderWithModel<TModel> : IShouldPassForTestBuilderWithInvokedAction
    {
        IShouldPassForTestBuilderWithModel<TModel> TheModel(Action<TModel> assertions);

        IShouldPassForTestBuilderWithModel<TModel> TheModel(Func<TModel, bool> predicate);
    }
}
