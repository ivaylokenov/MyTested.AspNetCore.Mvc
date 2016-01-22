namespace MyTested.Mvc.Tests.Setups.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.FileProviders;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.ViewEngines;
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

        public ICollection<ResponseModel> ResponseModel
        {
            get { return this.responseModel; }
        }

        public IInjectedService InjectedService { get; private set; }

        public IAnotherInjectedService AnotherInjectedService { get; private set; }

        public RequestModel InjectedRequestModel { get; private set; }

        public IActionResult DefaultView()
        {
            return this.View();
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
                ContentType = new MediaTypeHeaderValue(ContentType.ApplicationXml),
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

        public IActionResult IndexPartialView()
        {
            return this.PartialView("_IndexPartial", this.responseModel);
        }

        public IActionResult CustomPartialViewResult()
        {
            return new PartialViewResult
            {
                StatusCode = 500,
                ContentType = new MediaTypeHeaderValue(ContentType.ApplicationXml),
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
            return this.ViewComponent("TestComponent", 1, "text");
        }

        public IActionResult ViewComponentResultByType()
        {
            return this.ViewComponent(typeof(CustomViewComponent), this.responseModel);
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
                ContentType = new MediaTypeHeaderValue(ContentType.ApplicationXml),
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

        public IActionResult CustomRequestAction()
        {
            if (this.Request.Method == "POST" && this.Request.Headers.ContainsKey("TestHeader"))
            {
                return this.Ok();
            }

            return this.HttpBadRequest();
        }

        public void EmptyAction()
        {
        }

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

        public IActionResult OkResultAction()
        {
            return this.Ok();
        }

        public IActionResult StatusCodeAction()
        {
            return new HttpStatusCodeResult(405);
        }

        public IActionResult ChallengeResultAction()
        {
            return this.Challenge();
        }

        public IActionResult ChallengeWithAuthenticationSchemes()
        {
            return this.Challenge(new List<string> { AuthenticationScheme.Basic, AuthenticationScheme.NTLM });
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
            return this.Forbid(new List<string> { AuthenticationScheme.Basic, AuthenticationScheme.NTLM });
        }

        public IActionResult ForbidWithAuthenticationProperties()
        {
            return this.Forbid(TestObjectFactory.GetAuthenticationProperties());
        }

        public IActionResult ForbidWithEmptyAuthenticationProperties()
        {
            return this.Forbid(TestObjectFactory.GetEmptyAuthenticationProperties());
        }

        public IActionResult FileWithVirtualPath()
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
            return new HttpOkObjectResult(5);
        }

        public void EmptyActionWithException()
        {
            this.ThrowNewNullReferenceException();
        }

        public IActionResult NullAction()
        {
            return null;
        }

        public int DefaultStructAction()
        {
            return 0;
        }

        public IActionResult ContentAction()
        {
            var mediaTypeHeaderValue = new MediaTypeHeaderValue(ContentType.ApplicationJson)
            {
                Encoding = Encoding.ASCII
            };

            return new ContentResult
            {
                Content = "content",
                ContentType = mediaTypeHeaderValue,
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

        public async Task<HttpOkResult> AsyncOkResultAction()
        {
            return await Task.Run(() => this.Ok());
        }

        public IActionResult BadRequestAction()
        {
            return this.HttpBadRequest();
        }

        public IActionResult BadRequestWithErrorAction()
        {
            return this.HttpBadRequest("Bad request");
        }

        public IActionResult BadRequestWithModelState(RequestModel model)
        {
            if (this.ModelState.IsValid)
            {
                return this.Ok();
            }

            return this.HttpBadRequest(this.ModelState);
        }

        public IActionResult BadRequestWithCustomError()
        {
            return this.HttpBadRequest(this.responseModel);
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
                ContentType = new MediaTypeHeaderValue(ContentType.ApplicationXml)
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

        public IActionResult CustomModelStateError()
        {
            this.ModelState.AddModelError("Test", "Test error");
            return this.Ok(this.responseModel);
        }

        public IActionResult HttpNotFoundAction()
        {
            return this.HttpNotFound();
        }

        public IActionResult HttpNotFoundWithObjectAction()
        {
            return this.HttpNotFound("test");
        }

        [Authorize]
        public IActionResult AuthorizedAction()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Ok();
            }

            return this.HttpNotFound();
        }

        public IActionResult UnauthorizedAction()
        {
            return this.HttpUnauthorized();
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

        private void ThrowNewNullReferenceException()
        {
            throw new NullReferenceException("Test exception message");
        }
    }
}
