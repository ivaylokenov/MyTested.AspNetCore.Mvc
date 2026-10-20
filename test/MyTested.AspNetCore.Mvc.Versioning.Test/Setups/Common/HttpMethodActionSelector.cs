namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Routing;

    public class HttpMethodActionSelector : IActionSelector
    {
        public IReadOnlyList<ActionDescriptor> SelectCandidates(RouteContext context)
            => new ActionDescriptor[0];

        public ActionDescriptor SelectBestCandidate(RouteContext context, IReadOnlyList<ActionDescriptor> candidates)
        {
            var requestMethod = context.HttpContext.Request.Method;

            return candidates?.FirstOrDefault(candidate => candidate
                .ActionConstraints?
                .OfType<HttpMethodActionConstraint>()
                .All(constraint => constraint.HttpMethods.Contains(requestMethod)) ?? true);
        }
    }
}
