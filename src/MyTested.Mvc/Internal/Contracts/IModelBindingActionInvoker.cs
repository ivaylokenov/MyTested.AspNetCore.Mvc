namespace MyTested.Mvc.Internal.Contracts
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc.Abstractions;

    public interface IModelBindingActionInvoker : IActionInvoker
    {
        IDictionary<string, object> BoundActionArguments { get; }
    }
}
