namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Http;
    using Builders.Http;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Contains <see cref="IAntiforgery"/> extension methods for <see cref="IHttpRequestBuilder"/>.
    /// </summary>
    public static class HttpRequestBuilderViewFeaturesExtensions
    {
        /// <summary>
        /// Adds anti-forgery token to the <see cref="Microsoft.AspNetCore.Http.HttpRequest"/>.
        /// </summary>
        /// <param name="httpRequestBuilder">Instance of <see cref="IHttpRequestBuilder"/> type.</param>
        /// <returns>The same <see cref="IHttpRequestBuilder"/>.</returns>
        public static IAndHttpRequestBuilder WithAntiForgeryToken(this IHttpRequestBuilder httpRequestBuilder)
        {
            var actualHttpRequestBuilder = (HttpRequestBuilder)httpRequestBuilder;

            var httpContext = actualHttpRequestBuilder.HttpContext;

            var antiForgery = httpContext.RequestServices.GetRequiredService<IAntiforgery>();
            var antiForgeryOptions = httpContext.RequestServices.GetRequiredService<IOptions<AntiforgeryOptions>>().Value;

            var tokens = antiForgery.GetTokens(httpContext);
            
            if (antiForgeryOptions.Cookie.Name != null)
            {
                actualHttpRequestBuilder.WithCookie(antiForgeryOptions.Cookie.Name, tokens.CookieToken);
            }

            if (antiForgeryOptions.HeaderName != null)
            {
                actualHttpRequestBuilder.WithHeader(antiForgeryOptions.HeaderName, tokens.RequestToken);
            }
            else
            {
                actualHttpRequestBuilder.WithFormField(antiForgeryOptions.FormFieldName, tokens.RequestToken);
            }

            //var generatedAntiforgeryFeature = httpContext.Features.Get<IAntiforgeryFeature>();

            //httpContext.Features.Set<IAntiforgeryFeature>(new AntiforgeryFeature
            //{
            //    HaveDeserializedCookieToken = false,
            //    HaveDeserializedRequestToken = false,
            //    CookieToken = generatedAntiforgeryFeature.NewCookieToken,
            //    RequestToken = generatedAntiforgeryFeature.NewRequestToken
            //});

            return actualHttpRequestBuilder;
        }
    }
}
