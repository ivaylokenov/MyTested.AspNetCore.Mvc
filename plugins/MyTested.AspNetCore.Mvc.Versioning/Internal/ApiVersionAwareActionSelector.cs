namespace MyTested.AspNetCore.Mvc.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    internal class ApiVersionAwareActionSelector : IActionSelector
    {
        private readonly IActionSelector inner;

        public ApiVersionAwareActionSelector(IActionSelector inner)
        {
            this.inner = inner;
        }

        public IReadOnlyList<ActionDescriptor> SelectCandidates(RouteContext context)
            => this.inner.SelectCandidates(context);

        public ActionDescriptor SelectBestCandidate(
            RouteContext context,
            IReadOnlyList<ActionDescriptor> candidates)
        {
            var requestedVersion = GetRequestedApiVersion(context);

            if (requestedVersion == null)
            {
                return this.inner.SelectBestCandidate(context, candidates);
            }

            var versionedCandidates = new List<ActionDescriptor>();

            foreach (var candidate in candidates)
            {
                var metadata = candidate.GetApiVersionMetadata();

                if (metadata == ApiVersionMetadata.Empty
                    || metadata.IsApiVersionNeutral
                    || metadata.IsMappedTo(requestedVersion))
                {
                    versionedCandidates.Add(candidate);
                }
            }

            if (versionedCandidates.Count == 0)
            {
                return null;
            }

            if (versionedCandidates.Count > 1)
            {
                var explicitlyMapped = versionedCandidates
                    .Where(c => c.GetApiVersionMetadata().MappingTo(requestedVersion) == ApiVersionMapping.Explicit)
                    .ToList();

                if (explicitlyMapped.Count > 0)
                {
                    versionedCandidates = explicitlyMapped;
                }
            }

            return this.inner.SelectBestCandidate(context, versionedCandidates);
        }

        private static ApiVersion GetRequestedApiVersion(RouteContext context)
        {
            var parser = ApiVersionParser.Default;

            if (context.RouteData.Values.TryGetValue("version", out var routeVersion)
                && routeVersion is string versionString
                && parser.TryParse(versionString, out var parsedVersion))
            {
                return parsedVersion;
            }

            var reader = context.HttpContext.RequestServices
                ?.GetService<IOptions<ApiVersioningOptions>>()
                ?.Value
                ?.ApiVersionReader;

            if (reader != null)
            {
                var rawVersions = reader.Read(context.HttpContext.Request);

                if (rawVersions.Count > 0
                    && parser.TryParse(rawVersions[0], out var parsedReaderVersion))
                {
                    return parsedReaderVersion;
                }
            }

            return null;
        }
    }
}
