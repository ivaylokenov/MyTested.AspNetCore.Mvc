namespace MyTested.AspNetCore.Mvc.Internal.Actions
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;

#pragma warning disable ASPDEPR006 // Obsolete IActionContextAccessor, still required by the framework's ControllerActionInvoker.
    public class ActionContextAccessorMock
    {
        internal static readonly IActionContextAccessor Null = new NullActionContextAccessor();

        private class NullActionContextAccessor : IActionContextAccessor
        {
            public ActionContext ActionContext
            {
                get => null;
                set { }
            }
        }
    }
#pragma warning restore ASPDEPR006 // Obsolete IActionContextAccessor, still required by the framework's ControllerActionInvoker.
}
