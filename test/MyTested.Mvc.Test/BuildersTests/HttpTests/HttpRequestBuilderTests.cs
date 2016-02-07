namespace MyTested.Mvc.Tests.BuildersTests.HttpTests
{
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.AspNetCore.Http.Features.Internal;
    using Microsoft.AspNetCore.Http.Internal;
    using Setups.Controllers;
    using Xunit;
    using Setups.Models;
    public class HttpRequestBuilderTests
    {
        [Fact]
        public void WithHttpRequestShouldWorkCorrectlyWithDefaultValues()
        {
            var stream = new MemoryStream();
            var requestCookies = new RequestCookieCollection(
                new Dictionary<string, string>
                {
                    { "MyRequestCookie", "MyRequestCookieValue" },
                    { "AnotherRequestCookie", "AnotherRequestCookieValue" }
                });
            var files = new[]
            {
                new FormFile(stream, 0, 0, "FirstFile", "FirstFileName"),
                new FormFile(stream, 0, 0, "SecondFile", "SecondFileName"),
            };
            
            var builtRequest = MyMvc
                .Controller<MvcController>()
                .WithHttpRequest(request => request
                    .WithBody(stream)
                    .WithContentLength(1)
                    .WithContentType(ContentType.ApplicationJson)
                    .AndAlso()
                    .WithCookie("MyCookie", "MyCookieValue")
                    .WithCookies(new Dictionary<string, string>
                    {
                        { "MyDictCookie", "MyDictCookieValue" },
                        { "AnotherDictCookie", "AnotherDictCookieValue" }
                    })
                    .WithCookies(requestCookies)
                    .AndAlso()
                    .WithFormField("Field", "FieldValue")
                    .WithFormField("MultiField", "FirstFieldValue", "SecondFieldValue")
                    .WithFormFields(new Dictionary<string, IEnumerable<string>>
                    {
                        { "MyDictField", new[] { "MyDictFieldValue" } },
                        { "AnotherDictField", new[] { "AnotherDictFieldValue" } }
                    })
                    .AndAlso()
                    .WithFormFiles(files)
                    .AndAlso()
                    .WithHeader("MyHeader", "MyHeaderValue")
                    .WithHeader("MultiHeader", "FirstHeaderValue", "SecondHeaderValue")
                    .WithHeaders(new Dictionary<string, IEnumerable<string>>
                    {
                        { "MyDictHeader", new[] { "MyDictHeaderValue" } },
                        { "AnotherDictHeader", new[] { "AnotherDictHeaderValue" } }
                    })
                    .AndAlso()
                    .WithHost("mytestedasp.net")
                    .WithMethod("POST")
                    .WithPath("/all")
                    .WithPathBase("/api")
                    .WithProtocol("protocol")
                    .WithQueryString("?key=value&another=yetanother")
                    .WithHttps())
                .AndProvideTheHttpRequest();

            Assert.Same(stream, builtRequest.Body);
            Assert.Equal(1, builtRequest.ContentLength);
            Assert.Same(ContentType.ApplicationJson, builtRequest.ContentType);

            Assert.Equal(5, builtRequest.Cookies.Count);
            Assert.Equal("MyCookieValue", builtRequest.Cookies["MyCookie"]);
            Assert.Equal("MyDictCookieValue", builtRequest.Cookies["MyDictCookie"]);
            Assert.Equal("AnotherDictCookieValue", builtRequest.Cookies["AnotherDictCookie"]);
            Assert.Equal("MyRequestCookieValue", builtRequest.Cookies["MyRequestCookie"]);
            Assert.Equal("AnotherRequestCookieValue", builtRequest.Cookies["AnotherRequestCookie"]);

            Assert.Equal(4, builtRequest.Form.Count);
            Assert.Equal("FieldValue", builtRequest.Form["Field"]);
            Assert.Equal("FirstFieldValue,SecondFieldValue", builtRequest.Form["MultiField"]);
            Assert.Equal("MyDictFieldValue", builtRequest.Form["MyDictField"]);
            Assert.Equal("AnotherDictFieldValue", builtRequest.Form["AnotherDictField"]);

            Assert.Equal(2, builtRequest.Form.Files.Count);
            Assert.Same(files[0], builtRequest.Form.Files[0]);
            Assert.Same(files[1], builtRequest.Form.Files[1]);
            
            Assert.Equal(8, builtRequest.Headers.Count);
            Assert.Equal("MyHeaderValue", builtRequest.Headers["MyHeader"]);
            Assert.Equal("FirstHeaderValue,SecondHeaderValue", builtRequest.Headers["MultiHeader"]);
            Assert.Equal("MyDictHeaderValue", builtRequest.Headers["MyDictHeader"]);
            Assert.Equal("AnotherDictHeaderValue", builtRequest.Headers["AnotherDictHeader"]);

            Assert.Equal("mytestedasp.net", builtRequest.Host.Value);
            Assert.Equal("POST", builtRequest.Method);
            Assert.Equal("/all", builtRequest.Path);
            Assert.Equal("/api", builtRequest.PathBase);
            Assert.Equal("protocol", builtRequest.Protocol);
            Assert.Equal("?key=value&another=yetanother", builtRequest.QueryString.Value);
            Assert.Equal("https", builtRequest.Scheme);

            Assert.Equal(2, builtRequest.Query.Count);
            Assert.Equal("value", builtRequest.Query["key"]);
            Assert.Equal("yetanother", builtRequest.Query["another"]);
        }

        [Fact]
        public void WithQueryShouldWorkCorrectly()
        {
            var builtRequest = MyMvc
                .Controller<MvcController>()
                .WithHttpRequest(request => request
                    .WithQuery(new Dictionary<string, IEnumerable<string>>
                    {
                        { "MyDictQuery", new[] { "MyDictQueryValue" } },
                        { "AnotherDictQuery", new[] { "AnotherDictQueryValue" } }
                    }))
                .AndProvideTheHttpRequest();

            Assert.Equal(2, builtRequest.Query.Count);
            Assert.Equal("MyDictQueryValue", builtRequest.Query["MyDictQuery"]);
            Assert.Equal("AnotherDictQueryValue", builtRequest.Query["AnotherDictQuery"]);
        }

        [Fact]
        public void WithLocationShouldSetCorrectProperties()
        {
            var builtRequest = MyMvc
                .Controller<MvcController>()
                .WithHttpRequest(request => request.WithLocation("https://mytestesasp.net:1337/api/Projects/MyTested.Mvc?version=1.0"))
                .AndProvideTheHttpRequest();

            Assert.Equal("mytestesasp.net:1337", builtRequest.Host.Value);
            Assert.Equal("/api/Projects/MyTested.Mvc", builtRequest.Path);
            Assert.Equal("/api/Projects/MyTested.Mvc", builtRequest.PathBase);
            Assert.Equal("?version=1.0", builtRequest.QueryString.Value);
            Assert.Equal(1, builtRequest.Query.Count);
            Assert.Equal("1.0", builtRequest.Query["version"]);
            Assert.Equal("https", builtRequest.Scheme);
        }
        
        [Fact]
        public void WithStringBodyShouldWorkCorrectly()
        {
            var builtRequest = MyMvc
                .Controller<MvcController>()
                .WithHttpRequest(request => request
                    .WithStringBody("test"))
                .AndProvideTheHttpRequest();

            using (var reader = new StreamReader(builtRequest.Body))
            {
                var body = reader.ReadToEnd();
                Assert.Equal("test", body);
                Assert.Equal(ContentType.TextPlain, builtRequest.ContentType);
                Assert.Equal(reader.BaseStream.Length, builtRequest.ContentLength);
            }
        }

        [Fact]
        public void WithStringBodyShouldWorkCorrectlyAndRespectContentTypeAndLength()
        {
            var builtRequest = MyMvc
                .Controller<MvcController>()
                .WithHttpRequest(request => request
                    .WithContentType(ContentType.ApplicationXml)
                    .WithContentLength(100)
                    .WithStringBody("test"))
                .AndProvideTheHttpRequest();

            using (var reader = new StreamReader(builtRequest.Body))
            {
                var body = reader.ReadToEnd();
                Assert.Equal("test", body);
                Assert.Equal(ContentType.ApplicationXml, builtRequest.ContentType);
                Assert.Equal(100, builtRequest.ContentLength);
            }
        }

        [Fact]
        public void WithJsonBodyAsStringShouldWorkCorrectly()
        {
            var builtRequest = MyMvc
                .Controller<MvcController>()
                .WithHttpRequest(request => request
                    .WithJsonBody(@"{""id"":1}"))
                .AndProvideTheHttpRequest();

            using (var reader = new StreamReader(builtRequest.Body))
            {
                var body = reader.ReadToEnd();
                Assert.Equal(@"{""id"":1}", body);
                Assert.Equal(ContentType.ApplicationJson, builtRequest.ContentType);
                Assert.Equal(reader.BaseStream.Length, builtRequest.ContentLength);
            }
        }

        [Fact]
        public void WithJsonBodyAsStringShouldWorkCorrectlyAndRespectContentTypeAndLength()
        {
            var builtRequest = MyMvc
                .Controller<MvcController>()
                .WithHttpRequest(request => request
                    .WithContentType(ContentType.ApplicationXml)
                    .WithContentLength(100)
                    .WithJsonBody(@"{""id"":1}"))
                .AndProvideTheHttpRequest();

            using (var reader = new StreamReader(builtRequest.Body))
            {
                var body = reader.ReadToEnd();
                Assert.Equal(@"{""id"":1}", body);
                Assert.Equal(ContentType.ApplicationXml, builtRequest.ContentType);
                Assert.Equal(100, builtRequest.ContentLength);
            }
        }

        [Fact]
        public void WithJsonBodyShouldWorkCorrectly()
        {
            var builtRequest = MyMvc
                .Controller<MvcController>()
                .WithHttpRequest(request => request
                    .WithJsonBody(new RequestModel { Integer = 1, RequiredString = "Text" }))
                .AndProvideTheHttpRequest();

            using (var reader = new StreamReader(builtRequest.Body))
            {
                var body = reader.ReadToEnd();
                Assert.Equal(@"{""Integer"":1,""RequiredString"":""Text"",""NonRequiredString"":null,""NotValidateInteger"":0}", body);
                Assert.Equal(ContentType.ApplicationJson, builtRequest.ContentType);
                Assert.Equal(reader.BaseStream.Length, builtRequest.ContentLength);
            }
        }

        [Fact]
        public void WithJsonBodyShouldWorkCorrectlyAndRespectContentTypeAndLength()
        {
            var builtRequest = MyMvc
                .Controller<MvcController>()
                .WithHttpRequest(request => request
                    .WithContentType(ContentType.ApplicationXml)
                    .WithContentLength(100)
                    .WithJsonBody(@"{""id"":1}"))
                .AndProvideTheHttpRequest();

            using (var reader = new StreamReader(builtRequest.Body))
            {
                var body = reader.ReadToEnd();
                Assert.Equal(@"{""id"":1}", body);
                Assert.Equal(ContentType.ApplicationXml, builtRequest.ContentType);
                Assert.Equal(100, builtRequest.ContentLength);
            }
        }
    }
}
