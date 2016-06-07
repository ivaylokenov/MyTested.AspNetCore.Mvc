namespace MyTested.AspNetCore.Mvc.Internal.Contracts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Abstractions;

    public interface IModelBindingActionInvoker : IActionInvoker
    {
        IDictionary<string, object> BoundActionArguments { get; }
    }
}
