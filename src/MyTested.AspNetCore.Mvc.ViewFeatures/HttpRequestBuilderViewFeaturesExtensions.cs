namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Http;
    using Builders.Http;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Antiforgery.Internal;
    using Microsoft.Extensions.Options;

    public static class HttpRequestBuilderViewFeaturesExtensions
    {
        public static IAndHttpRequestBuilder WithAntiForgeryToken(this IHttpRequestBuilder httpRequestBuilder)
        {
            var actualHttpRequestBuilder = (HttpRequestBuilder)httpRequestBuilder;

            var httpContext = actualHttpRequestBuilder.HttpContext;

            var antiForgery = From.Services<IAntiforgery>();
            var antiForgeryOptions = From.Services<IOptions<AntiforgeryOptions>>().Value;

            var tokens = antiForgery.GetTokens(httpContext);

            if (antiForgeryOptions.CookieName != null)
            {
                actualHttpRequestBuilder.WithCookie(antiForgeryOptions.CookieName, tokens.CookieToken);
            }

            if (antiForgeryOptions.HeaderName != null)
            {
                actualHttpRequestBuilder.WithHeader(antiForgeryOptions.HeaderName, tokens.RequestToken);
            }
            else
            {
                actualHttpRequestBuilder.WithFormField(antiForgeryOptions.FormFieldName, tokens.RequestToken);
            }

            var generatedAntiforgeryFeature = httpContext.Features.Get<IAntiforgeryFeature>();

            httpContext.Features.Set<IAntiforgeryFeature>(new AntiforgeryFeature
            {
                HaveDeserializedCookieToken = false,
                HaveDeserializedRequestToken = false,
                CookieToken = generatedAntiforgeryFeature.NewCookieToken,
                RequestToken = generatedAntiforgeryFeature.NewRequestToken
            });

            return actualHttpRequestBuilder;
        }
    }
}
