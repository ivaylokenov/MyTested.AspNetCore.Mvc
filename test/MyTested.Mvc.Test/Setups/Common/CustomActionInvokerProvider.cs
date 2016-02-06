namespace MyTested.Mvc.Tests.Setups.Common
{
    using Microsoft.AspNet.Mvc.Abstractions;

    public class CustomActionInvokerProvider : IActionInvokerProvider
    {
        public int Order
        {
            get
            {
                return int.MaxValue;
            }
        }

        public void OnProvidersExecuted(ActionInvokerProviderContext context)
        {
        }

        public void OnProvidersExecuting(ActionInvokerProviderContext context)
        {
        }
    }
}
