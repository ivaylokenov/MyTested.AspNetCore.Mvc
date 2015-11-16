namespace MyTested.Mvc.Test.Setups.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Services;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Authorization;
    using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json;

    // TODO:
    [Authorize(Roles = "Admin,Moderator" /*Users = "John,George"*/)]
    [Route("Mvc")]
    //[RoutePrefix("/api/test")]
    internal class MvcController : Controller
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

        public IActionResult CommonHeaderAction()
        {
            // TODO: ?
            //if (this.Request.Headers.Accept.Contains(new MediaTypeWithQualityHeaderValue(MediaType.ApplicationJson)))
            //{
            //    return this.Ok();
            //}

            return this.HttpBadRequest();
        }

        // TODO: ?
        //public HttpResponseMessage HttpResponseMessageAction()
        //{
        //    var response = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        ReasonPhrase = "Custom reason phrase",
        //        Version = new Version(1, 1),
        //        Content = new ObjectContent(this.responseModel.GetType(), this.responseModel, TestObjectFactory.GetCustomMediaTypeFormatter()),
        //        RequestMessage = this.Request
        //    };

        //    response.Headers.Add("TestHeader", "TestHeaderValue");
        //    response.Content.Headers.Add("TestHeader", "TestHeaderValue");

        //    return response;
        //}

        //public HttpResponseMessage HttpResponseMessageGenericObjectContentAction()
        //{
        //    return new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new ObjectContent<IEnumerable<ResponseModel>>(this.responseModel, new JsonMediaTypeFormatter())
        //    };
        //}

        //public HttpResponseMessage HttpResponseMessageWithStringContent()
        //{
        //    return new HttpResponseMessage
        //    {
        //        Content = new StringContent("Test string")
        //    };
        //}

        //public HttpResponseMessage HttpResponseMessageWithResponseModelAction()
        //{
        //    return this.Request.CreateResponse(HttpStatusCode.BadRequest, this.responseModel);
        //}

        //public HttpResponseMessage HttpResponseMessageWithMediaTypeFormatter()
        //{
        //    return this.Request.CreateResponse(
        //        HttpStatusCode.OK,
        //        this.responseModel,
        //        TestObjectFactory.GetCustomMediaTypeFormatter());
        //}

        //public HttpResponseMessage HttpResponseMessageWithoutContent()
        //{
        //    return new HttpResponseMessage(HttpStatusCode.NoContent);
        //}

        //public HttpResponseMessage HttpResponseMessageWithMediaType()
        //{
        //    return this.Request.CreateResponse(
        //        HttpStatusCode.OK,
        //        this.responseModel,
        //        MediaType.ApplicationJson);
        //}

        //public HttpResponseMessage HttpResponseMessageWithFormatterAndMediaType()
        //{
        //    return this.Request.CreateResponse(
        //        HttpStatusCode.OK,
        //        this.responseModel,
        //        TestObjectFactory.GetCustomMediaTypeFormatter(),
        //        MediaType.ApplicationJson);
        //}

        //public HttpResponseMessage HttpResponseError()
        //{
        //    return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new InvalidOperationException("Error"));
        //}

        //public HttpResponseMessage HttpResponseErrorWithHttpError()
        //{
        //    return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new HttpError("Error"));
        //}

        //public HttpResponseMessage HttpResponseErrorWithModelState()
        //{
        //    return this.Request.CreateErrorResponse(HttpStatusCode.OK, this.ModelState);
        //}

        //public HttpResponseMessage HttpResponseErrorWithStringMessage()
        //{
        //    return this.Request.CreateErrorResponse(HttpStatusCode.OK, "Error");
        //}

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

        // TODO: ?
        [Authorize(Roles = "Admin,Moderator" /*Users = "John,George"*/)]
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

        public IActionResult OkResultWithContentNegotiatorAction()
        {
            // TODO: ?
            return new HttpOkObjectResult(
                5);
                //TestObjectFactory.GetCustomContentNegotiator(),
                //TestObjectFactory.GetCustomHttpRequestMessage(),
                //TestObjectFactory.GetFormatters());
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
            return this.Content("content");
            // return this.Content(HttpStatusCode.OK, this.responseModel);
        }

        public IActionResult ContentActionWithMediaType()
        {
            return this.Content("content", new MediaTypeHeaderValue("text/plain"));
            //return this.Content(
            //    HttpStatusCode.OK,
            //    this.responseModel,
            //    TestObjectFactory.GetCustomMediaTypeFormatter(),
            //    TestObjectFactory.MediaType);
        }

        //public IActionResult ContentActionWithNullMediaType()
        //{
        //    return this.Content(
        //        HttpStatusCode.OK,
        //        this.responseModel,
        //        TestObjectFactory.GetCustomMediaTypeFormatter());
        //}

        //public IActionResult ContentActionWithCustomFormatters()
        //{
        //    return new NegotiatedContentResult<int>(
        //        HttpStatusCode.OK,
        //        5,
        //        TestObjectFactory.GetCustomContentNegotiator(),
        //        TestObjectFactory.GetCustomHttpRequestMessage(),
        //        TestObjectFactory.GetFormatters());
        //}

        public IActionResult CreatedAction()
        {
            return this.Created(TestObjectFactory.GetUri().OriginalString, this.responseModel);
        }

        //public IActionResult CreatedActionWithCustomContentNegotiator()
        //{
        //    return new CreatedNegotiatedContentResult<ICollection<ResponseModel>>(
        //        TestObjectFactory.GetUri(),
        //        this.responseModel,
        //        TestObjectFactory.GetCustomContentNegotiator(),
        //        TestObjectFactory.GetCustomHttpRequestMessage(),
        //        TestObjectFactory.GetFormatters());
        //}

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

        //public IActionResult ActionWithHttpResponseException()
        //{
        //    throw new HttpResponseException(HttpStatusCode.NotFound);
        //}

        //public IActionResult ActionWithHttpResponseExceptionAndHttpResponseMessageException()
        //{
        //    throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.InternalServerError));
        //}

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

        //public IActionResult JsonWithEncodingAction()
        //{
        //    return this.Json(this.responseModel, new JsonSerializerSettings(), Encoding.ASCII);
        //}

        public IActionResult JsonWithSettingsAction()
        {
            return this.Json(this.responseModel, TestObjectFactory.GetJsonSerializerSettings());
        }

        public IActionResult JsonWithSpecificSettingsAction(JsonSerializerSettings jsonSerializerSettings)
        {
            return this.Json(this.responseModel, jsonSerializerSettings);
        }

        //public IActionResult ConflictAction()
        //{
        //    return this.Conflict();
        //}

        //public IActionResult StatusCodeAction()
        //{
        //    return this.StatusCode(HttpStatusCode.Redirect);
        //}

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

        //public IActionResult UnauthorizedActionWithChallenges()
        //{
        //    return this.Unauthorized(new[]
        //    {
        //        new AuthenticationHeaderValue("TestScheme", "TestParameter"),
        //        new AuthenticationHeaderValue("Basic"),
        //        new AuthenticationHeaderValue("YetAnotherScheme", "YetAnotherParameter")
        //    });
        //}

        //public IActionResult InternalServerErrorAction()
        //{
        //    return this.InternalServerError();
        //}

        //public IActionResult InternalServerErrorWithExceptionAction()
        //{
        //    try
        //    {
        //        this.ThrowNewNullReferenceException();
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        return this.InternalServerError(ex);
        //    }

        //    return this.Ok();
        //}

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

        private void ThrowNewNullReferenceException()
        {
            throw new NullReferenceException("Test exception message");
        }
    }
}
