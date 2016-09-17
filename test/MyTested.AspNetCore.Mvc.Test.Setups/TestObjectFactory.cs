namespace MyTested.AspNetCore.Mvc.Test.Setups
{
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.Serialization.Formatters;
    using Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Services;
    using System.IO;

    public static class TestObjectFactory
    {
        public const string MediaType = "application/json";

        public static Action<IServiceCollection> GetCustomServicesRegistrationAction()
            => services =>
            {
                services.AddTransient<IInjectedService, InjectedService>();
                services.AddTransient<IAnotherInjectedService, AnotherInjectedService>();
            };

        public static Action<IServiceCollection> GetCustomServicesWithOptionsRegistrationAction()
            => services =>
            {
                services.AddTransient<IInjectedService, InjectedService>();
                services.AddTransient<IAnotherInjectedService, AnotherInjectedService>();

                services.Configure<MvcOptions>(options =>
                {
                    options.MaxModelValidationErrors = 10;
                });
            };

        public static AuthenticationProperties GetAuthenticationProperties()
        {
            var authenticationProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1)),
                IsPersistent = true,
                IssuedUtc = new DateTimeOffset(new DateTime(2015, 1, 1, 1, 1, 1)),
                RedirectUri = "test"
            };

            authenticationProperties.Items.Add("TestKeyItem", "TestValueItem");
            authenticationProperties.Items.Add("AnotherTestKeyItem", "AnotherTestValueItem");

            return authenticationProperties;
        }

        public static IUrlHelper GetCustomUrlHelper() => new CustomUrlHelper();

        public static IViewEngine GetViewEngine() => new CustomViewEngine();

        public static AuthenticationProperties GetEmptyAuthenticationProperties() => new AuthenticationProperties();

        public static HttpRequest GetCustomHttpRequestMessage() => new DefaultHttpContext().Request;

        public static void SetCustomHttpResponseProperties(HttpResponse response)
        {
            response.Body = new MemoryStream();
            var writer = new StreamWriter(response.Body);
            writer.Write(@"{""Integer"":1,""RequiredString"":""Text""}");
            writer.Flush();

            response.ContentType = ContentType.ApplicationJson;
            response.StatusCode = 500;
            response.Headers.Add("TestHeader", "TestHeaderValue");
            response.Headers.Add("AnotherTestHeader", "AnotherTestHeaderValue");
            response.Headers.Add("MultipleTestHeader", new[] { "FirstMultipleTestHeaderValue", "AnotherMultipleTestHeaderValue" });
            response.Cookies.Append(
                "TestCookie",
                "TestCookieValue",
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Domain = "testdomain.com",
                    Expires = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1)),
                    Path = "/"
                });
            response.Cookies.Append(
                "AnotherCookie",
                "TestCookieValue",
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Domain = "testdomain.com",
                    Expires = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1)),
                    Path = "/"
                });
            response.ContentLength = 100;
        }

        public static IOutputFormatter GetOutputFormatter() => new JsonOutputFormatter(GetJsonSerializerSettings(), ArrayPool<char>.Create());

        public static Uri GetUri() => new Uri("http://somehost.com/someuri/1?query=Test");

        public static Action<string, string> GetFailingValidationActionWithTwoParameteres()
            => (x, y) => { throw new NullReferenceException($"{x} {y}"); };

        public static Action<string, string, string> GetFailingValidationAction()
            => (x, y, z) => { throw new NullReferenceException($"{x} {y} {z}"); };

        public static RequestModel GetNullRequestModel() => null;

        public static RequestModel GetValidRequestModel()
            => new RequestModel
            {
                Integer = 2,
                RequiredString = "Test"
            };

        public static RequestModel GetRequestModelWithErrors() => new RequestModel();

        public static List<ResponseModel> GetListOfResponseModels()
            => new List<ResponseModel>
            {
                new ResponseModel { IntegerValue = 1, StringValue = "Test" },
                new ResponseModel { IntegerValue = 2, StringValue = "Another Test" }
            };

        public static IFileProvider GetFileProvider() => new CustomFileProvider();

        public static JsonSerializerSettings GetJsonSerializerSettings()
             => new JsonSerializerSettings
             {
                 Culture = CultureInfo.InvariantCulture,
                 ContractResolver = new CamelCasePropertyNamesContractResolver(),
                 ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                 DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                 DateFormatString = "TEST",
                 DateParseHandling = DateParseHandling.DateTimeOffset,
                 DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                 DefaultValueHandling = DefaultValueHandling.Ignore,
                 EqualityComparer = new CustomEqualityComparer(),
                 Formatting = Formatting.Indented,
                 FloatFormatHandling = FloatFormatHandling.String,
                 FloatParseHandling = FloatParseHandling.Decimal,
                 MaxDepth = 2,
                 MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
                 MissingMemberHandling = MissingMemberHandling.Ignore,
                 NullValueHandling = NullValueHandling.Ignore,
                 ReferenceResolverProvider = () => new CustomJsonReferenceResolver(),
                 ObjectCreationHandling = ObjectCreationHandling.Replace,
                 PreserveReferencesHandling = PreserveReferencesHandling.Arrays,
                 ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                 StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                 TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                 TypeNameHandling = TypeNameHandling.None,
                 TraceWriter = new CustomJsonTraceWriter()
             };
    }
}
