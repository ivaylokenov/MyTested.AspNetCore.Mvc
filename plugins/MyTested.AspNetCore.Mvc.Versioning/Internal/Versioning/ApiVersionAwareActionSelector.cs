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
                return null;
            }

            if (requestedApiVersion == null)
            {
                if (!string.IsNullOrEmpty(apiVersioningFeature.RawRequestedApiVersion))
                {
                    return null;
                }

                var options = this.optionsAccessor.Value;
                if (options.AssumeDefaultVersionWhenUnspecified)
                {
                    requestedApiVersion = SelectApiVersion(options, context.HttpContext.Request, candidates);
                    apiVersioningFeature.RequestedApiVersion = requestedApiVersion;
                }
            }

            var mappedCandidates = candidates
                .Where(c => IsUnversioned(c)
                    || c.ApiVersionMetadata.IsApiVersionNeutral
                    || c.ApiVersionMetadata.MappingTo(requestedApiVersion) != ApiVersionMapping.None)
                .ToArray();

            if (mappedCandidates.Length == 0)
            {
                return null;
            }

            // Explicitly mapped actions win over implicitly mapped ones, but only among the
            // actions which satisfy the action constraints of the request (such as the HTTP method).
            // An action explicitly mapped for one HTTP method must not hide an implicitly
            // mapped action for another HTTP method on the same route template.
            var explicitCandidates = mappedCandidates
                .Where(c => IsUnversioned(c) || IsExplicitlyMapped(c, requestedApiVersion))
                .ToArray();

            if (explicitCandidates.Length > 0 && explicitCandidates.Length < mappedCandidates.Length)
            {
                var explicitMatch = this.ActionSelector.SelectBestCandidate(context, explicitCandidates);
                if (explicitMatch != null)
                {
                    return explicitMatch;
                }
            }

            return this.ActionSelector.SelectBestCandidate(context, mappedCandidates);
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
    }
}
