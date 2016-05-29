namespace MyTested.Mvc.Test.Setups.Common
{
    using Microsoft.AspNetCore.Mvc.Abstractions;

    public class CustomActionInvokerProvider : IActionInvokerProvider
    {
        public int Order => int.MaxValue;

        public void OnProvidersExecuted(ActionInvokerProviderContext context)
        {
        }

        public void OnProvidersExecuting(ActionInvokerProviderContext context)
        {
        }
    }
}
