namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ViewComponents
{
    using Base;

    public interface IViewComponentBuilder<TViewComponent> : IBaseTestBuilder
        where TViewComponent : class
    {
    }
}
