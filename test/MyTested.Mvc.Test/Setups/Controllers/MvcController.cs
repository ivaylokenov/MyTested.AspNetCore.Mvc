namespace MyTested.Mvc.Tests.Setups.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.FileProviders;
    using Microsoft.AspNet.Mvc;
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

        public IActionResult ChallengeResultAction()
        {
            return this.Challenge();
        }

        public IActionResult ChallengeWithAuthenticationSchemes()
        {
            return this.Challenge(new List<string> { AuthenticationScheme.Basic, AuthenticationScheme.NTLM });
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

        public IActionResult FileWithContents()
        {
            return this.File(new byte[] { 1, 2, 3 }, ContentType.ApplicationJson);
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
            return new ContentResult
            {
                Content = "content",
                ContentType = new MediaTypeHeaderValue(ContentType.ApplicationJson),
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

        public IActionResult CreatedAtRouteAction()
        {
            return this.CreatedAtRoute("Redirect", new { action = "WithParameter", id = 1 }, this.responseModel);
        }

        public IActionResult CreatedAtRouteVoidAction()
        {
            return this.CreatedAtRoute("Redirect", new { action = "VoidAction" }, this.responseModel);
        }

        public IActionResult RedirectAction()
        {
            return this.Redirect(TestObjectFactory.GetUri().OriginalString);
        }

        public IActionResult RedirectActionWithUri()
        {
            return this.Redirect(TestObjectFactory.GetUri().OriginalString);
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

        public IActionResult JsonAction()
        {
            return this.Json(this.responseModel);
        }
        
        public IActionResult JsonWithSettingsAction()
        {
            return this.Json(this.responseModel, TestObjectFactory.GetJsonSerializerSettings());
        }

        public IActionResult JsonWithSpecificSettingsAction(JsonSerializerSettings jsonSerializerSettings)
        {
            return this.Json(this.responseModel, jsonSerializerSettings);
        }
        
        public IActionResult CustomModelStateError()
        {
            this.ModelState.AddModelError("Test", "Test error");
            return this.Ok(this.responseModel);
        }

        public IActionResult NotFoundAction()
        {
            return this.HttpNotFound();
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
