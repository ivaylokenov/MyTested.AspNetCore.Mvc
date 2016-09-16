# HTTP Request
---

On various test cases HTTP request may need to be configured.

For example controller tests:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithHeader("MyHeader", "MyHeaderValue"))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
``` 

Or route tests:

```c#
MyMvc
	.Routes()
	.ShouldMap(request => request
		.WithLocation("/My/Action/1")
		.WithMethod(HttpMethod.Post)
		.WithJsonBody(@"{""First"":2,""Second"":""MyValue""}"))
	.To<MyController>(c => c.Action(1, new RequestModel
	{
		First = 2,
		Second = "MyValue"
	}));
```

## Providing HttpRequest
---

You can provide HttpRequest object directly:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(myHttpRequestObject) // object of HttpRequest type
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

## Setting Body
---

### Body As Stream

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithBody(myStream))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Body As Object With Content Type

Uses registered MVC formatters to convert the provided object as stream with the provided content type. Default encoding is UTF8.

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithBody(myBodyObject, "application/json"))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

> NOTE
>
> Instead of "application/json" string you may use [ContentType.ApplicationJson](/api/MyTested.Mvc.ContentType.html#MyTested_Mvc_ContentType)

### Body As Object With Content Type And Encoding

Uses registered MVC formatters to convert the provided object to stream with the provided content type and encoding.

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithBody(myBodyObject, ContentType.ApplicationJson, Encoding.ASCII))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Body As String

Sets the body as string. If no content type is set on the request, "text/plain" will be used. Default encoding is UTF8.

Default content type:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithStringBody("My Body")) // uses "text/plain" content type
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

Custom content type:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithContentType("text/css")
		.WithStringBody("My Body")) // uses "text/css" content type
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Body As String With Encoding

Sets the body as string with the provided encoding. If no content type is set on the request, "text/plain" will be used.

Default content type:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithStringBody("My Body", Encoding.ASCII)) // uses "text/plain" content type
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

Custom content type:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithContentType("text/css")
		.WithStringBody("My Body", Encoding.ASCII)) // uses "text/css" content type
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Body As JSON String

Sets the body as JSON string. Sets the content type to "application/json". Uses default encoding UTF8.

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithJsonBody(@"{""First"":2,""Second"":""MyValue""}"))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Body As JSON String With Encoding

Sets the body as JSON string with the provided encoding. Sets the content type to "application/json".

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithJsonBody(@"{""First"":2,""Second"":""MyValue""}", Encoding.ASCII))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Body As JSON Object

Sets the body as JSON object and uses MVC formatters to convert it to stream. Sets the content type to "application/json". Uses default encoding UTF8.

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithJsonBody(new { First = 1, Second = "MyValue" }))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Body As JSON Object With Encoding

Sets the body as JSON object and uses MVC formatters to convert it to stream with the provided encoding. Sets the content type to "application/json".

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithJsonBody(new { First = 1, Second = "MyValue" }, Encoding.ASCII))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

## Setting Content Length
---

Sets the content length of the HTTP request.

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithContentLength(100))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

## Setting Content Type
---

Sets the content type of the HTTP request.

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithContentType("application/xml"))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

> NOTE
>
> Instead of setting the content type as string you may use the [ContentType](/api/MyTested.Mvc.ContentType.html) class

## Setting Cookies
---

### Setting Single Cookie

Setting single cookie by name and value

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithCookie("MyCookieName", "MyCookieValue"))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

Or setting multiple cookies with more than one call

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithCookie("MyCookieName", "MyCookieValue")
		.WithCookie("AnotherCookieName", "AnotherCookieValue")
		.WithCookie("ThirdCookieName", "ThirdCookieValue"))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Setting Multiple Cookies With Dictionary

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithCookies(new Dictionary<string, string>
		{
			["MyCookieName"] = "MyCookieValue",
			["AnotherCookieName"] = "AnotherCookieValue"
		}))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Setting Multiple Cookies With Anonymous Object

Underscores (_) in property names will be replaced with dashes (-).

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithCookies(new 
		{
			My_Cookie_Name = "MyCookieValue", // "My-Cookie-Name"
			Another_Cookie_Name = "AnotherCookieValue" // "Another-Cookie-Name"
		}))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Setting Multiple Cookies With IRequestCookieCollection

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithCookies(myRequestCookieCollection)) // object of IRequestCookieCollection type
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

## Setting Form Values
---

### Setting Form Value

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithFormField("MyFormFieldName", "MyFormFieldValue"))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Setting Multiple Values To A Single Field

By IEnumerable&lt;string&gt;:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithFormField(
			"MyFormFieldName",
			new[] { "MyFormFieldValue", "AnotherFormFieldValue" }))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

By string parameters:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithFormField("MyFormFieldName", "MyFormFieldValue", "AnotherFormFieldValue"))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

By StringValues collection:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithFormField(
			"MyFormFieldName", 
			new StringValues("MyFormFieldValue", "AnotherFormFieldValue")))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Setting Multiple Fields With Dictionary

By IDictionary&lt;string, string&gt;:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithFormFields(new IDictionary<string, string>
		{
			["MyFormFieldName"] = "MyFormFieldValue",
			["AnotherFormFieldName"] = "AnotherFormFieldValue"
		}))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

By IDictionary&lt;string, IEnumerable&lt;string&gt;&gt;:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithFormFields(new IDictionary<string, IEnumerable<string>>
		{
			["MyFormFieldName"] = new[] { "MyFormFieldValue", "SecondFormFieldValue" },
			["AnotherFormFieldName"] = new[] { "AnotherFormFieldValue" }
		}))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

By IDictionary&lt;string, StringValues&gt;:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithFormFields(new IDictionary<string, StringValues>
		{
			["MyFormFieldName"] = 
				new StringValues("MyFormFieldValue", "SecondFormFieldValue"),
			["AnotherFormFieldName"] = 
				new StringValues("AnotherFormFieldValue")
		}))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Setting Multiple Fields With Anonymous Object

Underscores (_) in property names will be replaced with dashes (-).

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithFormFields(new 
		{
			My_FormField_Name = "MyFormFieldValue", // "My-FormField-Name"
			Another_FormField_Name = "AnotherFormFieldValue" // "Another-FormField-Name"
		}))
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Setting Form File With IFormFile

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithFormFile(myFormFile)) // object of IFormFile type
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Setting Multiple Form Files

By IEnumerable&lt;IFormFile&gt;:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithFormFiles(myFormFiles)) // IEnumerable of IFormFile
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

By IFormFile parameters:

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithFormFiles(myFormFile, anotherFormFile)) // Objects of IFormFile type
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

### Setting Form By IFormCollection

```c#
MyMvc
	.Controller<MyController>()
	.WithHttpRequest(request => request
		.WithForm(myForm)) // Objects of IFormCollection type
	.Calling(c => c.MyAction())
	.ShouldReturn()
	.View();
```

## Setting Headers
---