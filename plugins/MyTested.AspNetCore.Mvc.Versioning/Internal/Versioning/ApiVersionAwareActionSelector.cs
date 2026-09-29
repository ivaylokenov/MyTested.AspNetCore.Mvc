namespace MyTested.AspNetCore.Mvc.Internal.Versioning
{
    using System.Collections.Generic;
    using System.Linq;
    using Asp.Versioning;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Options;
    using Utilities.Validators;

    public class ApiVersionAwareActionSelector : IActionSelector
    {
        private readonly IOptions<ApiVersioningOptions> optionsAccessor;

        public ApiVersionAwareActionSelector(
            IActionSelector actionSelector,
            IOptions<ApiVersioningOptions> optionsAccessor)
        {
            CommonValidator.CheckForNullReference(actionSelector, nameof(actionSelector));
            CommonValidator.CheckForNullReference(optionsAccessor, nameof(optionsAccessor));

            this.ActionSelector = actionSelector;
            this.optionsAccessor = optionsAccessor;
        }

        public IActionSelector ActionSelector { get; }

        public IReadOnlyList<ActionDescriptor> SelectCandidates(RouteContext context)
            => this.ActionSelector.SelectCandidates(context);

        public ActionDescriptor SelectBestCandidate(
            RouteContext context,
            IReadOnlyList<ActionDescriptor> candidates)
        {
            // API versioning applies only when at least one of the candidates is versioned.
            if (candidates == null || candidates.All(IsUnversioned))
            {
                return this.ActionSelector.SelectBestCandidate(context, candidates);
            }

            var apiVersioningFeature = context.HttpContext.ApiVersioningFeature;

            ApiVersion requestedApiVersion;

            try
            {
                requestedApiVersion = apiVersioningFeature.RequestedApiVersion;
            }
            catch (AmbiguousApiVersionException)
            {
                // Multiple different API versions are requested.
                return null;
            }

            if (requestedApiVersion == null)
            {
                if (!string.IsNullOrEmpty(apiVersioningFeature.RawRequestedApiVersion))
                {
                    // The requested API version is malformed.
                    return null;
                }

                var options = this.optionsAccessor.Value;
                if (options.AssumeDefaultVersionWhenUnspecified)
                {
                    requestedApiVersion = SelectApiVersion(options, context.HttpContext.Request, candidates);
                    apiVersioningFeature.RequestedApiVersion = requestedApiVersion;
                }
            }

            var matchingCandidates = MatchApiVersion(candidates, requestedApiVersion);
            if (matchingCandidates.Count == 0)
            {
                return null;
            }

            return this.ActionSelector.SelectBestCandidate(context, matchingCandidates);
        }

        private static bool IsUnversioned(ActionDescriptor candidate)
            => candidate.ApiVersionMetadata == ApiVersionMetadata.Empty;

        private static bool IsExplicitlyMapped(ActionDescriptor candidate, ApiVersion apiVersion)
        {
            var metadata = candidate.ApiVersionMetadata;
            var mapping = metadata.MappingTo(apiVersion);

            return mapping == ApiVersionMapping.Explicit
                || (mapping == ApiVersionMapping.Implicit && metadata.IsApiVersionNeutral);
        }

        private static ApiVersion SelectApiVersion(
            ApiVersioningOptions options,
            HttpRequest request,
            IEnumerable<ActionDescriptor> candidates)
        {
            var model = candidates
                .Where(c => !IsUnversioned(c))
                .Select(c => c.ApiVersionMetadata.Map(ApiVersionMapping.Explicit))
                .Aggregate();

            return options.ApiVersionSelector.SelectVersion(request, model);
        }

        // Mirrors the endpoint routing API version matching. Unversioned candidates are always valid,
        // explicitly mapped and version-neutral candidates take precedence over the implicitly mapped ones,
        // and all other candidates are rejected.
        private static IReadOnlyList<ActionDescriptor> MatchApiVersion(
            IReadOnlyList<ActionDescriptor> candidates,
            ApiVersion apiVersion)
        {
            var hasExplicitMatch = candidates.Any(c => IsExplicitlyMapped(c, apiVersion));

            return candidates
                .Where(c => IsUnversioned(c)
                    || IsExplicitlyMapped(c, apiVersion)
                    || (!hasExplicitMatch && c.ApiVersionMetadata.MappingTo(apiVersion) == ApiVersionMapping.Implicit))
                .ToArray();
        }
    }
}
