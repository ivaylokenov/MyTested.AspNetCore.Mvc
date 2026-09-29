namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Routing;

    public class CustomActionSelector : IActionSelector
    {
        public IReadOnlyList<ActionDescriptor> Candidates { get; set; } = Array.Empty<ActionDescriptor>();

        public IReadOnlyList<ActionDescriptor> SelectCandidates(RouteContext context)
            => this.Candidates;

        public ActionDescriptor SelectBestCandidate(RouteContext context, IReadOnlyList<ActionDescriptor> candidates)
            => candidates?.FirstOrDefault();
    }
}
