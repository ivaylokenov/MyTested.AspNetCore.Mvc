namespace MyTested.Mvc.Test.Setups
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.Serialization.Formatters;
    using Common;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Authentication;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Services;

    public static class TestObjectFactory
    {
        public const string MediaType = "application/json";
        
        public static Action<IServiceCollection> GetCustomServicesRegistrationAction()
        {
            return services =>
            {
                services.AddTransient<IInjectedService, InjectedService>();
                services.AddTransient<IAnotherInjectedService, AnotherInjectedService>();
            };
        }

        public static Action<IServiceCollection> GetCustomServicesWithOptionsRegistrationAction()
        {
            return services =>
            {
                services.AddTransient<IInjectedService, InjectedService>();
                services.AddTransient<IAnotherInjectedService, AnotherInjectedService>();
                
                services.Configure<MvcOptions>(options =>
                {
                    options.MaxModelValidationErrors = 10;
                });
            };
        }

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

        public static IUrlHelper GetCustomUrlHelper()
        {
            return new CustomUrlHelper();
        }

        public static IViewEngine GetViewEngine()
        {
            return new CustomViewEngine();
        }

        public static AuthenticationProperties GetEmptyAuthenticationProperties()
        {
            return new AuthenticationProperties();
        }

        public static HttpRequest GetCustomHttpRequestMessage()
        {
            return new DefaultHttpContext().Request;
        }
        
        public static IOutputFormatter GetOutputFormatter()
        {
            return new JsonOutputFormatter();
        }

        public static Uri GetUri()
        {
            return new Uri("http://somehost.com/someuri/1?query=Test");
        }

        public static Action<string, string> GetFailingValidationActionWithTwoParameteres()
        {
            return (x, y) => { throw new NullReferenceException($"{x} {y}"); };
        }

        public static Action<string, string, string> GetFailingValidationAction()
        {
            return (x, y, z) => { throw new NullReferenceException($"{x} {y} {z}"); };
        }

        public static RequestModel GetNullRequestModel()
        {
            return null;
        }

        public static RequestModel GetValidRequestModel()
        {
            return new RequestModel
            {
                Integer = 2,
                RequiredString = "Test"
            };
        }

        public static RequestModel GetRequestModelWithErrors()
        {
            return new RequestModel();
        }

        public static List<ResponseModel> GetListOfResponseModels()
        {
            return new List<ResponseModel>
            {
                new ResponseModel { IntegerValue = 1, StringValue = "Test" },
                new ResponseModel { IntegerValue = 2, StringValue = "Another Test" }
            };
        }

        public static IFileProvider GetFileProvider()
        {
            return new CustomFileProvider();
        }

        public static JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                Culture = CultureInfo.InvariantCulture,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateParseHandling = DateParseHandling.DateTimeOffset,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented,
                MaxDepth = 2,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                PreserveReferencesHandling = PreserveReferencesHandling.Arrays,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                TypeNameHandling = TypeNameHandling.None
            };
        }
    }
}
