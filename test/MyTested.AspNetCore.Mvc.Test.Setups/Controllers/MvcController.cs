namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Builders.Authentication;
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Net.Http.Headers;
    using Models;
    using Pipelines;
    using Newtonsoft.Json;
    using Services;
    using ActionFilters;

    [Authorize(Roles = "Admin,Moderator")]
    [FormatFilter]
    [ValidateAntiForgeryToken]
    [Route("/api/test")]
    public class MvcController : Controller
    {
        public MvcController()
            : this(new InjectedService())
        {
        }

        public MvcController(IInjectedService injectedService)
        {
            this.InjectedService = injectedService;
            this.ResponseModel = TestObjectFactory.GetListOfResponseModels();
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

        public ICollection<ResponseModel> ResponseModel { get; }

        public IInjectedService InjectedService { get; }

        public IAnotherInjectedService AnotherInjectedService { get; }

        public RequestModel InjectedRequestModel { get; }

        public IActionResult DefaultView()
        {
            return this.View();
        }

        public IActionResult DefaultViewWithModel()
        {
            return this.View(this.ResponseModel);
        }

        public IActionResult IndexView()
        {
            return this.View("Index", this.ResponseModel);
        }

        public IActionResult ViewResultByName()
        {
            return this.View("TestView", new { id = 1, test = "text" });
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

        public IActionResult NullViewComponent()
        {
            return this.ViewComponent((string)null);
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
            return this.PartialView(this.ResponseModel);
        }

        public IActionResult IndexPartialView()
        {
            return this.PartialView("_IndexPartial", this.ResponseModel);
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

        public IActionResult CustomPartialViewResultWithViewData()
        {
            var partialView = new PartialViewResult
            {
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            };

            partialView.ViewData.Model = TestObjectFactory.GetListOfResponseModels();
            return partialView;
        }

        public IActionResult ViewComponentResultByName()
        {
            return this.ViewComponent("TestComponent", new { id = 1, test = "text" });
        }

        public IActionResult ViewComponentResultByType()
        {
            return this.ViewComponent(typeof(CustomViewComponent), new { model = this.ResponseModel });
        }

        public IActionResult ViewComponentWithIncorrectArguments()
        {
            return new ViewComponentResult
            {
                Arguments = this.ResponseModel
            };
        }

        public IActionResult CustomViewComponentResult()
        {
            return new ViewComponentResult
            {
                StatusCode = 500,
                ContentType = ContentType.ApplicationXml
            };
        }

        public IActionResult CustomViewComponentResultWithViewData()
        {
            var viewComponent = new ViewComponentResult
            {
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            };

            viewComponent.ViewData.Model = TestObjectFactory.GetListOfResponseModels();
            return viewComponent;
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
            return new BadRequestObjectResult(this.ResponseModel)
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) },
                StatusCode = StatusCodes.Status201Created,
                Formatters = new FormatterCollection<IOutputFormatter> { TestObjectFactory.GetOutputFormatter(), new CustomOutputFormatter() },
                DeclaredType = typeof(List<ResponseModel>),
            };
        }

        public IActionResult HttpBadRequestActionWithFormatter(IOutputFormatter formatter)
        {
            return new BadRequestObjectResult(this.ResponseModel)
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
        [SkipStatusCodePages]
        [ResponseCache(
            CacheProfileName = "Test Profile",
            Duration = 30,
            Location = ResponseCacheLocation.Client,
            VaryByHeader = "Test Header",
            VaryByQueryKeys = new[] { "FirstQuery", "SecondQuery" },
            NoStore = true,
            Order = 2)]
        [MiddlewareFilter(typeof(MyPipeline), Order = 2)]
        [ServiceFilter(typeof(MyActionFilter), Order = 2)]
        [TypeFilter(typeof(MyActionFilterWithArgs), Order = 2, Arguments = new object[] { 10 })]
        public IActionResult VariousAttributesAction()
        {
            return this.Ok();
        }

        [Consumes("application/json", "application/xml")]
        [Produces("application/json", Type = typeof(ResponseModel), Order = 1)]
        public IActionResult ConsumesAction()
        {
            return this.Ok();
        }

        [Area("InArea")]
        public IActionResult OtherAttributes()
        {
            return this.Ok();
        }

        [ValidateAntiForgeryToken]
        public IActionResult AntiForgeryToken()
        {
            return this.Ok();
        }

        [RequestFormLimits(
            BufferBody = false,
            BufferBodyLengthLimit = 10,
            KeyLengthLimit = 20,
            MemoryBufferThreshold = 30,
            MultipartBodyLengthLimit = 40,
            MultipartBoundaryLengthLimit = 50,
            MultipartHeadersCountLimit = 60,
            MultipartHeadersLengthLimit = 70,
            Order = 80,
            ValueCountLimit = 90,
            ValueLengthLimit = 100)]
        public IActionResult RequestFormLimits()
        {
            return this.Ok();
        }

        [RequestSizeLimit(1024)]
        public IActionResult RequestSizeLimit()
        {
            return this.Ok();
        }

        [DisableRequestSizeLimit]
        public IActionResult DisabledRequestSizeLimit()
        {
            return this.Ok();
        }

        [IgnoreAntiforgeryToken]
        public IActionResult IgnoreAntiForgeryToken()
        {
            return this.Ok();
        }

        public IActionResult OkResultAction()
        {
            return this.Ok();
        }

        public IActionResult FullOkAction()
        {
            return new OkObjectResult(this.ResponseModel)
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) },
                StatusCode = StatusCodes.Status201Created,
                Formatters = new FormatterCollection<IOutputFormatter> { TestObjectFactory.GetOutputFormatter(), new CustomOutputFormatter() },
                DeclaredType = typeof(List<ResponseModel>),
            };
        }

        public IActionResult FullObjectResultAction()
        {
            return new ObjectResult(this.ResponseModel)
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) },
                StatusCode = StatusCodes.Status201Created,
                Formatters = new FormatterCollection<IOutputFormatter> { TestObjectFactory.GetOutputFormatter(), new CustomOutputFormatter() },
                DeclaredType = typeof(List<ResponseModel>),
            };
        }

        public IActionResult OkActionWithFormatter(IOutputFormatter formatter)
        {
            return new OkObjectResult(this.ResponseModel)
            {
                Formatters = new FormatterCollection<IOutputFormatter> { formatter }
            };
        }

        public IActionResult ObjectActionWithFormatter(IOutputFormatter formatter)
        {
            return new ObjectResult(this.ResponseModel)
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

        public IActionResult SignInWithAuthenticationPropertiesAndScheme()
        {
            return this.SignIn(ClaimsPrincipalBuilder.DefaultAuthenticated,
                TestObjectFactory.GetAuthenticationProperties(),
                AuthenticationScheme.Basic);
        }

        public IActionResult SignInWithEmptyAuthenticationPropertiesAndScheme()
        {
            return this.SignIn(ClaimsPrincipalBuilder.DefaultAuthenticated,
                TestObjectFactory.GetEmptyAuthenticationProperties(),
                AuthenticationScheme.Basic);
        }

        public IActionResult SignOutWithAuthenticationSchemes()
        {
            return this.SignOut(AuthenticationScheme.Basic, AuthenticationScheme.NTLM);
        }

        public IActionResult SignOutWithAuthenticationProperties()
        {
            return this.SignOut(TestObjectFactory.GetAuthenticationProperties());
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
            return this.Created(TestObjectFactory.GetUri().OriginalString, this.ResponseModel);
        }

        public IActionResult FullCreatedAction()
        {
            return new CreatedResult(TestObjectFactory.GetUri(), this.ResponseModel)
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) },
                StatusCode = StatusCodes.Status201Created,
                Formatters = new FormatterCollection<IOutputFormatter> { TestObjectFactory.GetOutputFormatter(), new CustomOutputFormatter() },
                DeclaredType = typeof(List<ResponseModel>),
            };
        }

        public IActionResult CreatedActionWithFormatter(IOutputFormatter formatter)
        {
            return new CreatedResult(TestObjectFactory.GetUri(), this.ResponseModel)
            {
                Formatters = new FormatterCollection<IOutputFormatter> { formatter }
            };
        }

        public IActionResult CreatedActionWithUri()
        {
            return this.Created(TestObjectFactory.GetUri(), this.ResponseModel);
        }

        public IActionResult CreatedAtActionResult()
        {
            return this.CreatedAtAction("MyAction", "MyController", new { id = 1, text = "sometext" }, this.ResponseModel);
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
                this.ResponseModel)
            {
                UrlHelper = urlHelper
            };
        }

        public IActionResult CreatedAtRouteAction()
        {
            return this.CreatedAtRoute("Redirect", new { action = "WithParameter", id = 1 }, this.ResponseModel);
        }

        public IActionResult CreatedAtRouteVoidAction()
        {
            return this.CreatedAtRoute("Redirect", new { action = "VoidAction" }, this.ResponseModel);
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
            return this.Ok(this.ResponseModel);
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
            return this.Ok(this.ResponseModel);
        }

        public IActionResult OkResultWithResponse()
        {
            return this.Ok(this.ResponseModel.ToList());
        }

        public async Task<OkResult> AsyncOkResultAction()
        {
            return await Task.Run(() => this.Ok());
        }

        public IActionResult ObjectResultWithResponse()
        {
            return new ObjectResult(this.ResponseModel.ToList());
        }

        public IActionResult OkResultWithRepeatedName()
        {
            return this.Ok(new CustomActionResult());
        }

        public IActionResult OkResultWithRepeatedCollectionName()
        {
            return this.Ok(new List<CustomActionResult>
            {
                new CustomActionResult()
            });
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
            return this.BadRequest(this.ResponseModel);
        }

        public IActionResult AcceptedAction()
        {
            return this.Accepted();
        }

        public IActionResult FullAcceptedAction()
        {
            return new AcceptedResult()
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) }
            };
        }

        public IActionResult ConflictAction()
        {
            return this.Conflict();
        }

        public IActionResult FullConflictAction()
        {
            return new ConflictObjectResult(this.ModelState)
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) }
            };
        }

        public IActionResult UnprocessableEntityAction()
        {
            return this.UnprocessableEntity();
        }

        public IActionResult FullUnprocessableEntityAction()
        {
            return new UnprocessableEntityObjectResult(this.ModelState)
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) }
            };
        }

        public IActionResult ModelStateWithNestedError()
        {
            this.ModelState.AddModelError<NestedModel>(m => m.Nested.Integer, "NestedError");
            this.ModelState.AddModelError("Nested.String", "NestedStringError");

            return this.Ok();
        }

        public IActionResult JsonAction()
        {
            return this.Json(this.ResponseModel);
        }

        public IActionResult EmptyJsonAction()
        {
            return this.Json("{}");
        }

        public IActionResult NullJsonAction()
        {
            return this.Json(null);
        }

        public IActionResult JsonWithStatusCodeAction()
        {
            return new JsonResult(this.ResponseModel)
            {
                StatusCode = 200,
                ContentType = ContentType.ApplicationXml
            };
        }

        public IActionResult JsonWithSettingsAction()
        {
            return this.Json(this.ResponseModel, TestObjectFactory.GetJsonSerializerSettings());
        }

        public IActionResult JsonWithSpecificSettingsAction(JsonSerializerSettings jsonSerializerSettings)
        {
            return this.Json(this.ResponseModel, jsonSerializerSettings);
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
            return this.Ok(this.ResponseModel);
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
            return new NotFoundObjectResult(this.ResponseModel)
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) },
                StatusCode = StatusCodes.Status201Created,
                Formatters = new FormatterCollection<IOutputFormatter> { TestObjectFactory.GetOutputFormatter(), new CustomOutputFormatter() },
                DeclaredType = typeof(List<ResponseModel>),
            };
        }

        public IActionResult HttpNotFoundActionWithFormatter(IOutputFormatter formatter)
        {
            return new NotFoundObjectResult(this.ResponseModel)
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

        public IActionResult FullUnauthorizedAction()
        {
            return new UnauthorizedObjectResult(this.ResponseModel)
            {
                ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml) },
            };
        }

        public bool GenericStructAction()
        {
            return true;
        }

        public IResponseModel GenericInterfaceAction()
        {
            return this.ResponseModel.FirstOrDefault();
        }

        public ResponseModel GenericAction()
        {
            return this.ResponseModel.FirstOrDefault();
        }

        public ICollection<ResponseModel> GenericActionWithCollection()
        {
            return this.ResponseModel;
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
            await this.TryUpdateModelAsync(model);
            return this.Ok();
        }

        public IActionResult DistributedCacheAction()
        {
            var distributedCache = this.HttpContext.RequestServices.GetService<IDistributedCache>();

            var cacheValue = distributedCache.Get("test");
            if (cacheValue?.SequenceEqual(new byte[] { 127, 127, 127 }) ?? false)
            {
                return this.Ok();
            }

            return this.BadRequest();
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

        public IActionResult AddDistributedCacheActionWithStringValueEntries()
        {
            var distributedCache = this.HttpContext.RequestServices.GetService<IDistributedCache>();
            distributedCache.SetString("test", "testValue", new DistributedCacheEntryOptions
            { 
                AbsoluteExpiration = new DateTimeOffset(new DateTime(2020, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });

            distributedCache.SetString("another", "anotherValue");

            return this.Ok();
        }

        public IActionResult AddDistributedCacheAction()
        {
            var distributedCache = this.HttpContext.RequestServices.GetService<IDistributedCache>();
            distributedCache.Set("test", new byte[] { 127, 127, 127 }, new DistributedCacheEntryOptions
            { 
                AbsoluteExpiration = new DateTimeOffset(new DateTime(2020, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });

            distributedCache.Set("another", new byte[] { 4, 20 });

            return this.Ok();
        }

        public async Task<IActionResult> AddDistributedCacheActionAsync()
        {
            var distributedCache = this.HttpContext.RequestServices.GetService<IDistributedCache>();

            await distributedCache.SetAsync("test", new byte[] { 127, 127, 127});

            return this.Ok();
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

        public IActionResult GetSessionKeys()
        {
            return this.Ok(this.HttpContext.Session.Keys.ToList());
        }

        public IActionResult GetSessionKeysCount()
        {
            return this.Ok(this.HttpContext.Session.Keys.Count());
        }

        public object AnonymousResult()
        {
            return new
            {
                Id = 1,
                Text = "test",
                Nested = new
                {
                    IsTrue = true
                }
            };
        }

        public IActionResult AnonymousOkResult()
        {
            return this.Ok(new 
            { 
                Id = 1, 
                Text = "test",
                Nested = new
                {
                    IsTrue = true
                }
            });
        }

        public IActionResult WithService(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        public ActionResult<ResponseModel> ActionResultOfT(int id)
        {
            if (id == 0)
            {
                return this.BadRequest();
            }

            if (id == int.MaxValue)
            {
                return this.Ok(this.ResponseModel.First());
            }

            return this.ResponseModel.First();
        }

        public ActionResult<object> ActionResultOfAnonymousType()
        {
            return new
            {
                Id = 1,
                Text = "test",
                Nested = new
                {
                    IsTrue = true
                }
            };
        }

        private void ThrowNewNullReferenceException()
        {
            throw new NullReferenceException("Test exception message");
        }

        private void SetCustomResponse() => TestObjectFactory.SetCustomHttpResponseProperties(this.Response);
    }
}
