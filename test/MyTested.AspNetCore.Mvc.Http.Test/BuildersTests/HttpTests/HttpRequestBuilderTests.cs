namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.HttpTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class HttpRequestBuilderTests
    {
        [Fact]
        public void WithHttpRequestShouldWorkCorrectlyWithDefaultValues()
        {
            var stream = new MemoryStream();
            var requestCookies = new CustomRequestCookieCollection
            {
                ["MyRequestCookie"] = "MyRequestCookieValue",
                ["AnotherRequestCookie"] = "AnotherRequestCookieValue"
            };

            var files = new[]
            {
                new FormFile(stream, 0, 0, "FirstFile", "FirstFileName"),
                new FormFile(stream, 0, 0, "SecondFile", "SecondFileName"),
            };

            MyController<MvcController>
                .Instance()
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
                    .WithCookies(new
                    {
                        ObjectCookie = "ObjectCookieValue",
                        AnotherObjectCookie = "AnotherObjectCookieValue"  
                    })
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
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Same(stream, builtRequest.Body);
                    Assert.Equal(1, builtRequest.ContentLength);
                    Assert.Same(ContentType.ApplicationJson, builtRequest.ContentType);

                    Assert.Equal(7, builtRequest.Cookies.Count);
                    Assert.Equal("MyCookieValue", builtRequest.Cookies["MyCookie"]);
                    Assert.Equal("MyDictCookieValue", builtRequest.Cookies["MyDictCookie"]);
                    Assert.Equal("AnotherDictCookieValue", builtRequest.Cookies["AnotherDictCookie"]);
                    Assert.Equal("MyRequestCookieValue", builtRequest.Cookies["MyRequestCookie"]);
                    Assert.Equal("AnotherRequestCookieValue", builtRequest.Cookies["AnotherRequestCookie"]);
                    Assert.Equal("ObjectCookieValue", builtRequest.Cookies["ObjectCookie"]);
                    Assert.Equal("AnotherObjectCookieValue", builtRequest.Cookies["AnotherObjectCookie"]);

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
                });
        }

        [Fact]
        public void WithBodyShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithBody(
                        new
                        {
                            Id = 1,
                            String = "Test"
                        },
                        ContentType.ApplicationJson))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    using (var reader = new StreamReader(builtRequest.Body))
                    {
                        var result = reader.ReadToEnd();

                        Assert.Equal(@"{""id"":1,""string"":""Test""}", result);
                    }
                });
        }

        [Fact]
        public void WithFormAsDictionaryShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithFormFields(new Dictionary<string, IEnumerable<string>>
                    {
                        ["Field"] = new List<string> { "FieldValue" },
                        ["AnotherField"] = new List<string> { "AnotherFieldValue", "SecondFieldValue" },
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Form.Count);
                    Assert.Equal("FieldValue", builtRequest.Form["Field"]);
                    Assert.Equal("AnotherFieldValue,SecondFieldValue", builtRequest.Form["AnotherField"]);
                });
        }

        [Fact]
        public void WithFormAsObjectShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithFormFields(new
                    {
                        Field = "FieldValue",
                        AnotherField = "AnotherFieldValue"
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Form.Count);
                    Assert.Equal("FieldValue", builtRequest.Form["Field"]);
                    Assert.Equal("AnotherFieldValue", builtRequest.Form["AnotherField"]);
                });
        }
        
        [Fact]
        public void WithFormAsStringDictionaryShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithFormFields(new Dictionary<string, string>
                    {
                        ["Field"] = "FieldValue",
                        ["AnotherField"] = "AnotherFieldValue"
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Form.Count);
                    Assert.Equal("FieldValue", builtRequest.Form["Field"]);
                    Assert.Equal("AnotherFieldValue", builtRequest.Form["AnotherField"]);
                });
        }

        [Fact]
        public void WithFormAsDictionaryAndStringValuesShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithFormFields(new Dictionary<string, StringValues>
                    {
                        ["Field"] = new StringValues("FieldValue"),
                        ["AnotherField"] = new StringValues(new[] { "AnotherFieldValue", "SecondFieldValue" }),
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Form.Count);
                    Assert.Equal("FieldValue", builtRequest.Form["Field"]);
                    Assert.Equal("AnotherFieldValue,SecondFieldValue", builtRequest.Form["AnotherField"]);
                });
        }

        [Fact]
        public void WithFormShouldWorkCorrectly()
        {
            var stream = new MemoryStream();
            var files = new FormFileCollection
            {
                new FormFile(stream, 0, 0, "FirstFile", "FirstFileName"),
                new FormFile(stream, 0, 0, "SecondFile", "SecondFileName"),
            };

            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithForm(new FormCollection(
                        new Dictionary<string, StringValues>
                        {
                            ["Field"] = new StringValues("FieldValue"),
                            ["AnotherField"] = new StringValues(new[] { "AnotherFieldValue", "SecondFieldValue" }),
                        },
                        files)))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Form.Count);
                    Assert.Equal("FieldValue", builtRequest.Form["Field"]);
                    Assert.Equal("AnotherFieldValue,SecondFieldValue", builtRequest.Form["AnotherField"]);
                    Assert.Equal(2, builtRequest.Form.Files.Count);
                    Assert.Same(files[0], builtRequest.Form.Files[0]);
                    Assert.Same(files[1], builtRequest.Form.Files[1]);
                });
        }

        [Fact]
        public void WithHeadersAsDictionaryShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithHeaders(new Dictionary<string, IEnumerable<string>>
                    {
                        ["Header"] = new List<string> { "HeaderValue" },
                        ["AnotherHeader"] = new List<string> { "AnotherHeaderValue", "SecondHeaderValue" },
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Headers.Count);
                    Assert.Equal("HeaderValue", builtRequest.Headers["Header"]);
                    Assert.Equal("AnotherHeaderValue,SecondHeaderValue", builtRequest.Headers["AnotherHeader"]);
                });
        }
        
        [Fact]
        public void WithHeadersAsObjectDictionaryShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithHeaders(new
                    {
                        Header = "HeaderValue",
                        AnotherHeader = "AnotherHeaderValue"
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Headers.Count);
                    Assert.Equal("HeaderValue", builtRequest.Headers["Header"]);
                    Assert.Equal("AnotherHeaderValue", builtRequest.Headers["AnotherHeader"]);
                });
        }

        [Fact]
        public void WithHeadersAsStringDictionaryShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithHeaders(new Dictionary<string, string>
                    {
                        ["Header"] = "HeaderValue",
                        ["AnotherHeader"] = "AnotherHeaderValue"
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Headers.Count);
                    Assert.Equal("HeaderValue", builtRequest.Headers["Header"]);
                    Assert.Equal("AnotherHeaderValue", builtRequest.Headers["AnotherHeader"]);
                });
        }

        [Fact]
        public void WithHeadersAsDictionaryWithStringValuesShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithHeaders(new Dictionary<string, StringValues>
                    {
                        ["Header"] = new StringValues("HeaderValue"),
                        ["AnotherHeader"] = new StringValues(new[] { "AnotherHeaderValue", "SecondHeaderValue" }),
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Headers.Count);
                    Assert.Equal("HeaderValue", builtRequest.Headers["Header"]);
                    Assert.Equal("AnotherHeaderValue,SecondHeaderValue", builtRequest.Headers["AnotherHeader"]);
                });
        }

        [Fact]
        public void WithHeadersShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithHeaders(new HeaderDictionary(new Dictionary<string, StringValues>
                    {
                        ["Header"] = new StringValues("HeaderValue"),
                        ["AnotherHeader"] = new StringValues(new[] { "AnotherHeaderValue", "SecondHeaderValue" })
                    })))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Headers.Count);
                    Assert.Equal("HeaderValue", builtRequest.Headers["Header"]);
                    Assert.Equal("AnotherHeaderValue,SecondHeaderValue", builtRequest.Headers["AnotherHeader"]);
                });
        }

        [Fact]
        public void WithQueryAsKeyValuePairShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithQuery("Query", "QueryValue"))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(1, builtRequest.Query.Count);
                    Assert.Equal("QueryValue", builtRequest.Query["Query"]);
                });
        }

        [Fact]
        public void WithQueryAsKeyMultipleValuesPairShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithQuery("Query", "QueryValue", "AnotherQueryValue"))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(1, builtRequest.Query.Count);
                    Assert.Equal("QueryValue,AnotherQueryValue", builtRequest.Query["Query"]);
                });
        }

        [Fact]
        public void WithQueryShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithQuery(new Dictionary<string, IEnumerable<string>>
                    {
                        { "MyDictQuery", new[] { "MyDictQueryValue" } },
                        { "AnotherDictQuery", new[] { "AnotherDictQueryValue" } }
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Query.Count);
                    Assert.Equal("MyDictQueryValue", builtRequest.Query["MyDictQuery"]);
                    Assert.Equal("AnotherDictQueryValue", builtRequest.Query["AnotherDictQuery"]);
                });
        }

        [Fact]
        public void WithQueryAsStringDictionaryShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithQuery(new Dictionary<string, string>
                    {
                        { "MyDictQuery", "MyDictQueryValue" },
                        { "AnotherDictQuery", "AnotherDictQueryValue" }
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Query.Count);
                    Assert.Equal("MyDictQueryValue", builtRequest.Query["MyDictQuery"]);
                    Assert.Equal("AnotherDictQueryValue", builtRequest.Query["AnotherDictQuery"]);
                });
        }

        [Fact]
        public void WithQueryAsObjectShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithQuery(new
                    {
                        MyDictQuery = "MyDictQueryValue",
                        AnotherDictQuery = "AnotherDictQueryValue"
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Query.Count);
                    Assert.Equal("MyDictQueryValue", builtRequest.Query["MyDictQuery"]);
                    Assert.Equal("AnotherDictQueryValue", builtRequest.Query["AnotherDictQuery"]);
                });
        }

        [Fact]
        public void WithQueryAsDictionaryWithStringValuesShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithQuery(new Dictionary<string, StringValues>
                    {
                        { "MyDictQuery", new[] { "MyDictQueryValue" } },
                        { "AnotherDictQuery", new[] { "AnotherDictQueryValue" } }
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Query.Count);
                    Assert.Equal("MyDictQueryValue", builtRequest.Query["MyDictQuery"]);
                    Assert.Equal("AnotherDictQueryValue", builtRequest.Query["AnotherDictQuery"]);
                });
        }

        [Fact]
        public void WithQueryAsCollectionShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithQuery(new QueryCollection(new Dictionary<string, StringValues>
                    {
                        { "MyDictQuery", new[] { "MyDictQueryValue" } },
                        { "AnotherDictQuery", new[] { "AnotherDictQueryValue" } }
                    })))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal(2, builtRequest.Query.Count);
                    Assert.Equal("MyDictQueryValue", builtRequest.Query["MyDictQuery"]);
                    Assert.Equal("AnotherDictQueryValue", builtRequest.Query["AnotherDictQuery"]);
                });
        }

        [Fact]
        public void WithLocationShouldSetCorrectProperties()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request.WithLocation("https://mytestesasp.net:1337/api/Projects/MyTested.AspNetCore.Mvc?version=1.0"))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal("mytestesasp.net:1337", builtRequest.Host.Value);
                    Assert.Equal("/api/Projects/MyTested.AspNetCore.Mvc", builtRequest.Path);
                    Assert.Equal("/api/Projects/MyTested.AspNetCore.Mvc", builtRequest.PathBase);
                    Assert.Equal("?version=1.0", builtRequest.QueryString.Value);
                    Assert.Equal(1, builtRequest.Query.Count);
                    Assert.Equal("1.0", builtRequest.Query["version"]);
                    Assert.Equal("https", builtRequest.Scheme);
                });
        }

        [Fact]
        public void WithLocationAsBuilderShouldSetCorrectProperties()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithLocation(location => location
                        .WithHost("mytestesasp.net")
                        .WithPort(1337)
                        .WithScheme("https")
                        .WithAbsolutePath("api/Projects/MyTested.AspNetCore.Mvc")
                        .WithQuery("?version=1.0")))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal("mytestesasp.net:1337", builtRequest.Host.Value);
                    Assert.Equal("/api/Projects/MyTested.AspNetCore.Mvc", builtRequest.Path);
                    Assert.Equal("/api/Projects/MyTested.AspNetCore.Mvc", builtRequest.PathBase);
                    Assert.Equal("?version=1.0", builtRequest.QueryString.Value);
                    Assert.Equal(1, builtRequest.Query.Count);
                    Assert.Equal("1.0", builtRequest.Query["version"]);
                    Assert.Equal("https", builtRequest.Scheme);
                });
        }

        [Fact]
        public void WithLocationAsBuilderShouldThrowExceptionWithIncorrectQuery()
        {
            Test.AssertException<ArgumentException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithHttpRequest(request => request
                            .WithLocation(location => location
                                .WithHost("mytestesasp.net")
                                .WithPort(1337)
                                .WithScheme("https")
                                .WithAbsolutePath("api/Projects/MyTested.AspNetCore.Mvc")
                                .WithQuery("version=1.0")));
                },
                "Query string must start with the '?' symbol.");
        }

        [Fact]
        public void WithInvalidLocationAsBuilderShouldThrowExceptionWithIncorrectLocation()
        {
            Test.AssertException<InvalidHttpRequestException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithHttpRequest(request => request
                            .WithLocation("!@#invalid!@#"));
                },
                "When building HttpRequest expected location to be URI valid, but instead received '!@#invalid!@#'.");
        }

        [Fact]
        public void WithStringBodyShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithStringBody("test"))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    using (var reader = new StreamReader(builtRequest.Body))
                    {
                        var body = reader.ReadToEnd();
                        Assert.Equal("test", body);
                        Assert.Equal(ContentType.TextPlain, builtRequest.ContentType);
                        Assert.Equal(reader.BaseStream.Length, builtRequest.ContentLength);
                    }
                });            
        }

        [Fact]
        public void WithStringBodyShouldWorkCorrectlyAndRespectContentTypeAndLength()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithContentType(ContentType.ApplicationXml)
                    .WithContentLength(100)
                    .WithStringBody("test"))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    using (var reader = new StreamReader(builtRequest.Body))
                    {
                        var body = reader.ReadToEnd();
                        Assert.Equal("test", body);
                        Assert.Equal(ContentType.ApplicationXml, builtRequest.ContentType);
                        Assert.Equal(100, builtRequest.ContentLength);
                    }
                });
        }

        [Fact]
        public void WithJsonBodyAsStringShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithJsonBody(@"{""id"":1}"))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    using (var reader = new StreamReader(builtRequest.Body))
                    {
                        var body = reader.ReadToEnd();
                        Assert.Equal(@"{""id"":1}", body);
                        Assert.Equal(ContentType.ApplicationJson, builtRequest.ContentType);
                        Assert.Equal(reader.BaseStream.Length, builtRequest.ContentLength);
                    }
                });
        }

        [Fact]
        public void WithJsonBodyAsStringShouldWorkCorrectlyAndRespectContentTypeAndLength()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithContentType(ContentType.ApplicationXml)
                    .WithContentLength(100)
                    .WithJsonBody(@"{""id"":1}"))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    using (var reader = new StreamReader(builtRequest.Body))
                    {
                        var body = reader.ReadToEnd();
                        Assert.Equal(@"{""id"":1}", body);
                        Assert.Equal(ContentType.ApplicationXml, builtRequest.ContentType);
                        Assert.Equal(100, builtRequest.ContentLength);
                    }
                });
        }

        [Fact]
        public void WithJsonBodyShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithJsonBody(new RequestModel
                    {
                        Integer = 1,
                        RequiredString = "Text"
                    }))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    using (var reader = new StreamReader(builtRequest.Body))
                    {
                        var body = reader.ReadToEnd();
                        Assert.Equal(@"{""integer"":1,""requiredString"":""Text"",""nonRequiredString"":null,""notValidateInteger"":0}", body);
                        Assert.Equal(ContentType.ApplicationJson, builtRequest.ContentType);
                        Assert.Equal(reader.BaseStream.Length, builtRequest.ContentLength);
                    }
                });
        }

        [Fact]
        public void WithJsonBodyShouldWorkCorrectlyAndRespectContentTypeAndLength()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithContentType(ContentType.ApplicationXml)
                    .WithContentLength(100)
                    .WithJsonBody(@"{""id"":1}"))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    using (var reader = new StreamReader(builtRequest.Body))
                    {
                        var body = reader.ReadToEnd();
                        Assert.Equal(@"{""id"":1}", body);
                        Assert.Equal(ContentType.ApplicationXml, builtRequest.ContentType);
                        Assert.Equal(100, builtRequest.ContentLength);
                    }
                });
        }

        [Fact]
        public void WithLocationShouldWorkWithAbsoluteUri()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request.WithLocation("http://mytestedasp.net"))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal("mytestedasp.net:80", builtRequest.Host.Value);
                    Assert.Equal("/", builtRequest.PathBase);
                    Assert.Equal("http", builtRequest.Scheme);
                    Assert.Equal("/", builtRequest.Path);
                    Assert.Equal(string.Empty, builtRequest.QueryString.Value);
                });
        }

        [Fact]
        public void WithLocationShouldWorkWithAbsoluteUriAndQueryString()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request.WithLocation("http://mytestedasp.net?test=text"))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal("mytestedasp.net:80", builtRequest.Host.Value);
                    Assert.Equal("/", builtRequest.PathBase);
                    Assert.Equal("http", builtRequest.Scheme);
                    Assert.Equal("/", builtRequest.Path);
                    Assert.Equal("?test=text", builtRequest.QueryString.Value);
                });
        }

        [Fact]
        public void WithLocationShouldWorkWithRelativeUri()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request.WithLocation("/Home/Index"))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Null(builtRequest.Host.Value);
                    Assert.Equal("/Home/Index", builtRequest.PathBase);
                    Assert.Equal("http", builtRequest.Scheme);
                    Assert.Equal("/Home/Index", builtRequest.Path);
                    Assert.Equal(string.Empty, builtRequest.QueryString.Value);
                });
        }

        [Fact]
        public void WithLocationShouldWorkWithRelativeUriAndQueryString()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request.WithLocation("/Home/Index?test=text"))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Null(builtRequest.Host.Value);
                    Assert.Equal("/Home/Index", builtRequest.PathBase);
                    Assert.Equal("http", builtRequest.Scheme);
                    Assert.Equal("/Home/Index", builtRequest.Path);
                    Assert.Equal("?test=text", builtRequest.QueryString.Value);
                });
        }

        [Fact]
        public void WithLocationShouldWorkWithFullUriAndQueryString()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request.WithLocation("http://mytestedasp.net/Home/Index?test=text"))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    Assert.Equal("mytestedasp.net:80", builtRequest.Host.Value);
                    Assert.Equal("/Home/Index", builtRequest.PathBase);
                    Assert.Equal("http", builtRequest.Scheme);
                    Assert.Equal("/Home/Index", builtRequest.Path);
                    Assert.Equal("?test=text", builtRequest.QueryString.Value);
                });
        }
    }
}
