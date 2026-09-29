namespace MyTested.AspNetCore.Mvc.Internal
{
    using System.Linq;
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    internal class ApiVersionActionConstraint : IActionConstraint
    {
        public int Order => 0;

        public bool Accept(ActionConstraintContext context)
        {
            var requestedVersion = GetRequestedApiVersion(context.RouteContext);

            if (requestedVersion == null)
            {
                return true;
            }

            var metadata = context.CurrentCandidate.Action.GetApiVersionMetadata();

            if (metadata == ApiVersionMetadata.Empty || metadata.IsApiVersionNeutral)
            {
                return true;
            }

            if (!metadata.IsMappedTo(requestedVersion))
            {
                return false;
            }

            if (context.Candidates.Count <= 1)
            {
                return true;
            }

            var mapping = metadata.MappingTo(requestedVersion);
            if (mapping == ApiVersionMapping.Explicit)
            {
                return true;
            }

            var hasExplicitCandidate = context.Candidates.Any(c =>
            {
                if (c.Action == context.CurrentCandidate.Action)
                {
                    return false;
                }

                var otherMetadata = c.Action.GetApiVersionMetadata();
                return otherMetadata.MappingTo(requestedVersion) == ApiVersionMapping.Explicit;
            });

            return !hasExplicitCandidate;
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
