namespace MyTested.Mvc.Test.Setups.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Net.Http.Headers;
    using Models;
    using Newtonsoft.Json;
    using Services;

    [Authorize(Roles = "Admin,Moderator")]
    [Route("/api/test")]
    public class MvcController : Controller
    {
        private readonly ICollection<ResponseModel> responseModel;

        public MvcController()
            : this(new InjectedService())
        {
        }

        public MvcController(IInjectedService injectedService)
        {
            this.InjectedService = injectedService;
            this.responseModel = TestObjectFactory.GetListOfResponseModels();
        }

        public MvcController(RequestModel requestModel)
        {
            this.InjectedRequestModel = requestModel;
        }

        public MvcController(IInjectedService injectedService, RequestModel requestModel)
            : this(injectedService)
        {
            this.InjectedRequestModel = requestModel;
        }

        public MvcController(IInjectedService injectedService, IAnotherInjectedService anotherInjectedService)
            : this(injectedService)
        {
            this.AnotherInjectedService = anotherInjectedService;
        }

        public MvcController(IInjectedService injectedService, IAnotherInjectedService anotherInjectedService, RequestModel requestModel)
            : this(injectedService, anotherInjectedService)
        {
            this.InjectedRequestModel = requestModel;
        }

        public ICollection<ResponseModel> ResponseModel => this.responseModel;

        public IInjectedService InjectedService { get; private set; }

        public IAnotherInjectedService AnotherInjectedService { get; private set; }

        public RequestModel InjectedRequestModel { get; private set; }

        public IActionResult DefaultView()
        {
            return this.View();
        }
        
        public IActionResult DefaultViewWithModel()
        {
            return this.View(this.responseModel);
        }

        public IActionResult IndexView()
        {
            return this.View("Index", this.responseModel);
        }

        public IActionResult CustomViewResult()
        {
            return new ViewResult
            {
                StatusCode = 500,
                ContentType = ContentType.ApplicationXml,
                ViewEngine = new CustomViewEngine()
            };
        }

        public IActionResult ViewWithViewEngine(IViewEngine viewEngine)
        {
            return new ViewResult
            {
                ViewEngine = viewEngine
            };
        }

        public IActionResult DefaultPartialView()
        {
            return this.PartialView();
        }

        public IActionResult DefaultPartialViewWithModel()
        {
            return this.PartialView(this.responseModel);
        }

        public IActionResult IndexPartialView()
        {
            return this.PartialView("_IndexPartial", this.responseModel);
        }

        public IActionResult CustomPartialViewResult()
        {
            return new PartialViewResult
            {
                StatusCode = 500,
                ContentType = ContentType.ApplicationXml,
                ViewEngine = new CustomViewEngine()
            };
        }

        public IActionResult PartialViewWithViewEngine(IViewEngine viewEngine)
        {
            return new PartialViewResult
            {
                ViewEngine = viewEngine
            };
        }

        public IActionResult ViewComponentResultByName()
        {
            return this.ViewComponent("TestComponent", new { id = 1, test = "text" });
        }

        public IActionResult ViewComponentResultByType()
        {
            return this.ViewComponent(typeof(CustomViewComponent), new { model = this.responseModel });
        }

        public IActionResult ViewComponentWithIncorrectArguments()
        {
            return new ViewComponentResult
            {
                Arguments = this.responseModel
            };
        }

        public IActionResult CustomViewComponentResult()
        {
            return new ViewComponentResult
            {
                StatusCode = 500,
                ContentType = ContentType.ApplicationXml,
                ViewEngine = new CustomViewEngine()
            };
        }

        public IActionResult ViewComponentWithViewEngine(IViewEngine viewEngine)
        {
            return new ViewComponentResult
            {
                ViewEngine = viewEngine
            };
        }

        public IActionResult IndexOutOfRangeException()
        {
            throw new IndexOutOfRangeException();
        }

        public IActionResult CustomActionResult()
        {
            return new CustomActionResult();
        }

        public IActionResult CustomRequestAction()
        {
            if (this.Request.Method == "POST" && this.Request.Headers.ContainsKey("TestHeader"))
            {
                return this.Ok();
            }

            return this.BadRequest();
        }

        public void CustomVoidResponseAction()
        {
            this.SetCustomResponse();
        }

        public void CustomCookieHeadersAction(string cookie)
        {
            this.Response.Headers.Add("Set-Cookie", cookie);
        }

        public IActionResult CustomResponseAction()
        {
            this.SetCustomResponse();
            return this.Ok();
        }

        public IActionResult CustomResponseBodyWithBytesAction()
        {
            this.SetCustomResponse();
            this.Response.Body = new MemoryStream(new byte[] { 1, 2, 3 });
            return this.Ok();
        }

        public IActionResult CustomResponseBodyWithStringBody()
        {
            this.SetCustomResponse();
            this.Response.Body = new MemoryStream();
            this.Response.ContentType = ContentType.TextPlain;

            var writer = new StreamWriter(this.Response.Body);
            writer.Write("Test");
            writer.Flush();

            return this.Ok();
        }

        public IActionResult FullHttpBadRequestAction()
        {
            return new BadRequestObjectResult(this.responseModel)
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) },
                StatusCode = StatusCodes.Status201Created,
                Formatters = new FormatterCollection<IOutputFormatter> { new JsonOutputFormatter(), new CustomOutputFormatter() },
                DeclaredType = typeof(List<ResponseModel>),
            };
        }

        public IActionResult HttpBadRequestActionWithFormatter(IOutputFormatter formatter)
        {
            return new BadRequestObjectResult(this.responseModel)
            {
                Formatters = new FormatterCollection<IOutputFormatter> { formatter }
            };
        }

        public IActionResult EmptyResultAction()
        {
            return new EmptyResult();
        }

        public IActionResult NoContentResultAction()
        {
            return new NoContentResult();
        }

        public IActionResult UnsupportedMediaTypeResultAction()
        {
            return new UnsupportedMediaTypeResult();
        }

        public void EmptyAction()
        {
        }

        [ActionName("Test")]
        public void EmptyActionWithParameters(int id, RequestModel model)
        {
        }

        public async Task EmptyActionAsync()
        {
            await Task.Run(() => { });
        }

        [Authorize]
        [HttpGet]
        public void EmptyActionWithAttributes()
        {
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet]
        [HttpHead]
        public IActionResult NormalActionWithAttributes()
        {
            return this.Ok();
        }

        [AllowAnonymous]
        [Route("/api/test", Name = "TestRoute", Order = 1)]
        [ActionName("NormalAction")]
        [NonAction]
        [AcceptVerbs("Get", "Post")]
        [HttpDelete]
        public IActionResult VariousAttributesAction()
        {
            return this.Ok();
        }

        [ValidateAntiForgeryToken]
        public IActionResult AntiForgeryToken()
        {
            return this.Ok();
        }

        public IActionResult OkResultAction()
        {
            return this.Ok();
        }

        public IActionResult FullOkAction()
        {
            return new OkObjectResult(this.responseModel)
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) },
                StatusCode = StatusCodes.Status201Created,
                Formatters = new FormatterCollection<IOutputFormatter> { new JsonOutputFormatter(), new CustomOutputFormatter() },
                DeclaredType = typeof(List<ResponseModel>),
            };
        }

        public IActionResult FullObjectResultAction()
        {
            return new ObjectResult(this.responseModel)
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) },
                StatusCode = StatusCodes.Status201Created,
                Formatters = new FormatterCollection<IOutputFormatter> { new JsonOutputFormatter(), new CustomOutputFormatter() },
                DeclaredType = typeof(List<ResponseModel>),
            };
        }

        public IActionResult OkActionWithFormatter(IOutputFormatter formatter)
        {
            return new OkObjectResult(this.responseModel)
            {
                Formatters = new FormatterCollection<IOutputFormatter> { formatter }
            };
        }

        public IActionResult ObjectActionWithFormatter(IOutputFormatter formatter)
        {
            return new ObjectResult(this.responseModel)
            {
                Formatters = new FormatterCollection<IOutputFormatter> { formatter }
            };
        }

        public IActionResult StatusCodeAction()
        {
            return new StatusCodeResult(500);
        }

        public IActionResult ChallengeResultAction()
        {
            return this.Challenge();
        }

        public IActionResult ChallengeWithAuthenticationSchemes()
        {
            return this.Challenge(AuthenticationScheme.Basic, AuthenticationScheme.NTLM);
        }

        public IActionResult ChallengeWithAuthenticationProperties()
        {
            return this.Challenge(TestObjectFactory.GetAuthenticationProperties());
        }

        public IActionResult ChallengeWithEmptyAuthenticationProperties()
        {
            return this.Challenge(TestObjectFactory.GetEmptyAuthenticationProperties());
        }

        public IActionResult ForbidResultAction()
        {
            return this.Forbid();
        }

        public IActionResult ForbidWithAuthenticationSchemes()
        {
            return this.Forbid(AuthenticationScheme.Basic, AuthenticationScheme.NTLM);
        }

        public IActionResult ForbidWithAuthenticationProperties()
        {
            return this.Forbid(TestObjectFactory.GetAuthenticationProperties());
        }

        public IActionResult ForbidWithEmptyAuthenticationProperties()
        {
            return this.Forbid(TestObjectFactory.GetEmptyAuthenticationProperties());
        }

        public FileResult FileWithVirtualPath()
        {
            return this.File("/Test", ContentType.ApplicationJson, "FileDownloadName");
        }

        public IActionResult FileWithStream()
        {
            return this.File(new MemoryStream(new byte[] { 1, 2, 3 }), ContentType.ApplicationOctetStream);
        }

        public IActionResult FileWithFileProvider(IFileProvider fileProvider)
        {
            return new VirtualFileResult("Test", ContentType.ApplicationJson)
            {
                FileProvider = fileProvider ?? new CustomFileProvider()
            };
        }

        public IActionResult FileWithNullProvider()
        {
            return new VirtualFileResult("Test", ContentType.ApplicationJson);
        }

        public IActionResult FileWithContents()
        {
            return this.File(new byte[] { 1, 2, 3 }, ContentType.ApplicationJson);
        }

        public IActionResult PhysicalFileResult()
        {
            return this.PhysicalFile("/test/file", ContentType.ApplicationJson, "FileDownloadName");
        }

        public IActionResult OkResultWithContentNegotiatorAction()
        {
            return new OkObjectResult(5);
        }

        public void EmptyActionWithException()
        {
            this.ThrowNewNullReferenceException();
        }

        public IActionResult NullAction()
        {
            return null;
        }

        public IActionResult WithRouteData(int id)
        {
            if (this.RouteData.Values["controller"].ToString() == "Mvc"
                && this.RouteData.Values["action"].ToString() == "WithRouteData"
                && this.RouteData.Values["id"].ToString() == "1")
            {
                return this.View();
            }

            throw new InvalidOperationException();
        }

        public int DefaultStructAction()
        {
            return 0;
        }

        public IActionResult ContentAction()
        {
            return new ContentResult
            {
                Content = "content",
                ContentType = ContentType.ApplicationJson,
                StatusCode = 200
            };
        }

        public IActionResult ContentActionWithMediaType()
        {
            return this.Content("content", new MediaTypeHeaderValue("text/plain"));
        }

        public IActionResult ContentActionWithNullMediaType()
        {
            return this.Content("content", (MediaTypeHeaderValue)null);
        }

        public IActionResult CreatedAction()
        {
            return this.Created(TestObjectFactory.GetUri().OriginalString, this.responseModel);
        }

        public IActionResult FullCreatedAction()
        {
            return new CreatedResult(TestObjectFactory.GetUri(), this.responseModel)
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) },
                StatusCode = StatusCodes.Status201Created,
                Formatters = new FormatterCollection<IOutputFormatter> { new JsonOutputFormatter(), new CustomOutputFormatter() },
                DeclaredType = typeof(List<ResponseModel>),
            };
        }

        public IActionResult CreatedActionWithFormatter(IOutputFormatter formatter)
        {
            return new CreatedResult(TestObjectFactory.GetUri(), this.responseModel)
            {
                Formatters = new FormatterCollection<IOutputFormatter> { formatter }
            };
        }

        public IActionResult CreatedActionWithUri()
        {
            return this.Created(TestObjectFactory.GetUri(), this.responseModel);
        }

        public IActionResult CreatedAtActionResult()
        {
            return this.CreatedAtAction("MyAction", "MyController", new { id = 1, text = "sometext" }, this.responseModel);
        }

        public IActionResult CreatedAtActionWithCustomHelperResult(IUrlHelper urlHelper)
        {
            return new CreatedAtActionResult(
                "MyAction",
                "MyController",
                new
                {
                    id = 1,
                    text = "sometext"
                },
                this.responseModel)
            {
                UrlHelper = urlHelper
            };
        }

        public IActionResult CreatedAtRouteAction()
        {
            return this.CreatedAtRoute("Redirect", new { action = "WithParameter", id = 1 }, this.responseModel);
        }

        public IActionResult CreatedAtRouteVoidAction()
        {
            return this.CreatedAtRoute("Redirect", new { action = "VoidAction" }, this.responseModel);
        }

        public IActionResult RedirectPermanentAction()
        {
            return this.RedirectPermanent(TestObjectFactory.GetUri().OriginalString);
        }

        public IActionResult RedirectActionWithUri()
        {
            return this.Redirect(TestObjectFactory.GetUri().OriginalString);
        }

        public IActionResult RedirectToActionResult()
        {
            return new RedirectToActionResult("MyAction", "MyController", new { id = 1, text = "sometext" });
        }

        public IActionResult RedirectToActionWithCustomUrlHelperResult(IUrlHelper urlHelper)
        {
            return new RedirectToActionResult(
                "MyAction",
                "MyController",
                new
                {
                    id = 1,
                    text = "sometext"
                })
            {
                UrlHelper = urlHelper
            };
        }

        public IActionResult RedirectToRouteAction()
        {
            return this.RedirectToRoute("Redirect", new { action = "WithParameter", id = 1 });
        }

        public IActionResult RedirectToRouteVoidAction()
        {
            return this.RedirectToRoute("Redirect", new { action = "VoidAction" });
        }

        public IActionResult ActionWithException()
        {
            throw new NullReferenceException("Test exception message");
        }

        public IActionResult ActionWithAggregateException()
        {
            throw new AggregateException(new NullReferenceException(), new InvalidOperationException());
        }

        public async Task EmptyActionWithExceptionAsync()
        {
            await Task.Run(() => this.ThrowNewNullReferenceException());
        }

        public async Task<IActionResult> ActionWithExceptionAsync()
        {
            return await Task.Run(() =>
            {
                this.ThrowNewNullReferenceException();
                return this.Ok();
            });
        }

        public IActionResult OkResultActionWithRequestBody(int id, RequestModel model)
        {
            return this.Ok(this.responseModel);
        }

        public IActionResult WithRequest()
        {
            if (this.Request.Form.Any(f => f.Key == "Test"))
            {
                return this.Ok();
            }

            return this.BadRequest();
        }

        public IActionResult ModelStateCheck(RequestModel model)
        {
            if (ModelState.IsValid)
            {
                return this.Ok(model);
            }

            return this.Ok(model);
        }

        public IActionResult OkResultWithParameter(int id)
        {
            return this.Ok(id);
        }

        public IActionResult OkResultWithInterfaceResponse()
        {
            return this.Ok(this.responseModel);
        }

        public IActionResult OkResultWithResponse()
        {
            return this.Ok(this.responseModel.ToList());
        }

        public async Task<OkResult> AsyncOkResultAction()
        {
            return await Task.Run(() => this.Ok());
        }

        public IActionResult ObjectResultWithResponse()
        {
            return new ObjectResult(this.responseModel.ToList());
        }

        public IActionResult BadRequestAction()
        {
            return this.BadRequest();
        }

        public IActionResult BadRequestWithErrorAction()
        {
            return this.BadRequest("Bad request");
        }

        public IActionResult BadRequestWithModelState(RequestModel model)
        {
            if (this.ModelState.IsValid)
            {
                return this.Ok();
            }

            return this.BadRequest(this.ModelState);
        }

        public IActionResult BadRequestWithCustomError()
        {
            return this.BadRequest(this.responseModel);
        }

        public IActionResult ModelStateWithNestedError()
        {
            this.ModelState.AddModelError<NestedModel>(m => m.Nested.Integer, "NestedError");
            this.ModelState.AddModelError("Nested.String", "NestedStringError");

            return this.Ok();
        }

        public IActionResult JsonAction()
        {
            return this.Json(this.responseModel);
        }

        public IActionResult JsonWithStatusCodeAction()
        {
            return new JsonResult(this.responseModel)
            {
                StatusCode = 200,
                ContentType = ContentType.ApplicationXml
            };
        }

        public IActionResult JsonWithSettingsAction()
        {
            return this.Json(this.responseModel, TestObjectFactory.GetJsonSerializerSettings());
        }

        public IActionResult JsonWithSpecificSettingsAction(JsonSerializerSettings jsonSerializerSettings)
        {
            return this.Json(this.responseModel, jsonSerializerSettings);
        }

        public IActionResult LocalRedirectAction()
        {
            return this.LocalRedirect("/local/test");
        }

        public IActionResult LocalRedirectPermanentAction()
        {
            return this.LocalRedirectPermanent("/local/test");
        }

        public IActionResult LocalRedirectActionWithCustomUrlHelper(IUrlHelper helper)
        {
            return new LocalRedirectResult("/api/test")
            {
                UrlHelper = helper
            };
        }

        public IActionResult CustomModelStateError()
        {
            this.ModelState.AddModelError("Test", "Test error");
            return this.Ok(this.responseModel);
        }

        public IActionResult HttpNotFoundAction()
        {
            return this.NotFound();
        }

        public IActionResult HttpNotFoundWithObjectAction()
        {
            return this.NotFound("test");
        }

        public IActionResult FullHttpNotFoundAction()
        {
            return new NotFoundObjectResult(this.responseModel)
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) },
                StatusCode = StatusCodes.Status201Created,
                Formatters = new FormatterCollection<IOutputFormatter> { new JsonOutputFormatter(), new CustomOutputFormatter() },
                DeclaredType = typeof(List<ResponseModel>),
            };
        }

        public IActionResult HttpNotFoundActionWithFormatter(IOutputFormatter formatter)
        {
            return new NotFoundObjectResult(this.responseModel)
            {
                Formatters = new FormatterCollection<IOutputFormatter> { formatter }
            };
        }

        [Authorize]
        public IActionResult AuthorizedAction()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Ok();
            }

            return this.NotFound();
        }

        public IActionResult UnauthorizedAction()
        {
            return this.Unauthorized();
        }

        public bool GenericStructAction()
        {
            return true;
        }

        public IResponseModel GenericInterfaceAction()
        {
            return this.responseModel.FirstOrDefault();
        }

        public ResponseModel GenericAction()
        {
            return this.responseModel.FirstOrDefault();
        }

        public ICollection<ResponseModel> GenericActionWithCollection()
        {
            return this.responseModel;
        }

        public List<ResponseModel> GenericActionWithListCollection()
        {
            return TestObjectFactory.GetListOfResponseModels();
        }

        public dynamic DynamicResult()
        {
            return TestObjectFactory.GetListOfResponseModels();
        }

        public IActionResult UrlAction()
        {
            return this.Ok(this.Url.Action());
        }

        public IActionResult TryValidateModelAction()
        {
            var model = new RequestModel();
            this.TryValidateModel(model);
            if (this.ModelState.IsValid)
            {
                return this.Ok();
            }

            return this.BadRequest();
        }

        public async Task<IActionResult> TryUpdateModelAction()
        {
            var model = new RequestModel();
            await this.TryUpdateModelAsync<RequestModel>(model);
            return this.Ok();
        }

        public IActionResult MemoryCacheAction()
        {
            var memoryCache = this.HttpContext.RequestServices.GetService<IMemoryCache>();

            var cacheValue = memoryCache.Get<string>("test");
            if (cacheValue == "value")
            {
                return this.Ok();
            }

            return this.BadRequest();
        }
        
        public IActionResult FullSessionAction()
        {
            var session = this.HttpContext.Session;
            
            var hasId = session.GetString("HasId");
            if (!string.IsNullOrWhiteSpace(hasId) && hasId == "HasIdValue")
            {
                return this.Ok(session.Id);
            }

            var byteEntry = session.Get("ByteEntry");
            if (byteEntry != null)
            {
                return this.Ok(byteEntry);
            }

            var intEntry = session.GetInt32("IntEntry");
            if (intEntry != null)
            {
                return this.Ok(intEntry);
            }

            var stringEntry = session.GetString("StringEntry");
            if (stringEntry != null)
            {
                return this.Ok(stringEntry);
            }

            return this.BadRequest();
        }

        public IActionResult MultipleSessionValuesAction()
        {
            var session = this.HttpContext.Session;

            var stringValue = session.GetString("StringKey");
            var intValue = session.GetInt32("IntKey");
            var byteValue = session.Get("ByteKey");

            return this.Ok(new SessionResponseModel
            {
                String = stringValue,
                Integer = intValue.Value,
                Byte = byteValue
            });
        }

        public IActionResult AddMemoryCacheAction()
        {
            var memoryCache = this.HttpContext.RequestServices.GetService<IMemoryCache>();
            memoryCache.Set("test", "value", new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });

            memoryCache.Set("another", "anotherValue");

            return this.Ok();
        }

        public async Task<IActionResult> AddMemoryCacheActionAsync()
        {
            var memoryCache = this.HttpContext.RequestServices.GetService<IMemoryCache>();

            await Task.Run(() => memoryCache.Set("another", "anotherValue"));

            return this.Ok();
        }

        public IActionResult AddSessionAction()
        {
            this.HttpContext.Session.SetInt32("Integer", 1);
            this.HttpContext.Session.SetString("String", "Text");
            this.HttpContext.Session.Set("Bytes", new byte[] { 1, 2, 3 });
            return this.Ok();
        }

        public IActionResult AddTempDataAction()
        {
            this.TempData.Add("Test", "TempValue");
            this.TempData.Add("Another", "AnotherValue");
            return this.Ok();
        }
        
        public IActionResult AddViewBagAction()
        {
            this.ViewBag.Test = "BagValue";
            this.ViewBag.Another = "AnotherValue";
            return this.Ok();
        }

        public IActionResult AddViewDataAction()
        {
            this.ViewData["Test"] = "DataValue";
            this.ViewData["Another"] = "AnotherValue";
            return this.Ok();
        }

        public IActionResult TempDataAction()
        {
            var tempDataValue = this.TempData["Test"];
            if (tempDataValue != null)
            {
                return this.Ok(tempDataValue);
            }

            return this.BadRequest();
        }

        public IActionResult SessionAction()
        {
            if (this.HttpContext.Session.GetString("test") != null)
            {
                return this.Ok();
            }

            return this.BadRequest();
        }

        public IActionResult WithService(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        private void ThrowNewNullReferenceException()
        {
            throw new NullReferenceException("Test exception message");
        }

        private void SetCustomResponse()
        {
            var response = this.Response;
            response.Body = new MemoryStream();
            var writer = new StreamWriter(response.Body);
            writer.Write(@"{""Integer"":1,""RequiredString"":""Text""}");
            writer.Flush();

            response.ContentType = ContentType.ApplicationJson;
            response.StatusCode = HttpStatusCode.InternalServerError;
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
    }
}
