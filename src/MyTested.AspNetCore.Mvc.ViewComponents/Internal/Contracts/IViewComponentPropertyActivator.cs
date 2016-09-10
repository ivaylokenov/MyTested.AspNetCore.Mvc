namespace MyTested.AspNetCore.Mvc.Internal.Contracts
{
    using Microsoft.AspNetCore.Mvc.ViewComponents;

    public interface IViewComponentPropertyActivator
    {
        void Activate(ViewComponentContext viewComponentContext, object viewComponent);
    }
}
