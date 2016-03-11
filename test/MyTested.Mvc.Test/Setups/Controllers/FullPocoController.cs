namespace MyTested.Mvc.Test.Setups.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.DependencyInjection;
    using Common;
    using Microsoft.AspNetCore.Http;
    using Models;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Services;
    using Internal.Application;
    using Microsoft.AspNetCore.Http.Internal;

    public class FullPocoController
    {
        private readonly IServiceProvider services;
        private readonly ViewDataDictionary viewData;
        private readonly TempDataDictionary tempData;

        public FullPocoController()
        {
            this.services = TestApplication.Services;
            this.viewData = new ViewDataDictionary(services.GetService<IModelMetadataProvider>(), new ControllerContext().ModelState);
            this.tempData = (TempDataDictionary)services.GetService<ITempDataDictionaryFactory>().GetTempData(this.CustomHttpContext ?? new DefaultHttpContext());
        }
        
        public FullPocoController(IInjectedService injectedService)
        {
            this.InjectedService = injectedService;
        }

        public FullPocoController(RequestModel requestModel)
        {
            this.InjectedRequestModel = requestModel;
        }

        public FullPocoController(IInjectedService injectedService, RequestModel requestModel)
            : this(injectedService)
        {
            this.InjectedRequestModel = requestModel;
        }

        public FullPocoController(IInjectedService injectedService, IAnotherInjectedService anotherInjectedService)
            : this(injectedService)
        {
            this.AnotherInjectedService = anotherInjectedService;
        }

        public FullPocoController(IInjectedService injectedService, IAnotherInjectedService anotherInjectedService, RequestModel requestModel)
            : this(injectedService, anotherInjectedService)
        {
            this.InjectedRequestModel = requestModel;
        }

        public IInjectedService InjectedService { get; private set; }

        public IAnotherInjectedService AnotherInjectedService { get; private set; }

        public RequestModel InjectedRequestModel { get; private set; }

        public object PublicProperty { get; set; }

        public HttpContext CustomHttpContext => this.services.GetRequiredService<IHttpContextAccessor>().HttpContext;

        [ActionContext]
        public ActionContext CustomActionContext { get; set; }

        [ControllerContext]
        public ControllerContext CustomControllerContext { get; set; }

        [ViewDataDictionary]
        public ViewDataDictionary CustomViewData => this.viewData;

        public TempDataDictionary CustomTempData => this.tempData;

        public IUrlHelper CustomUrl => services.GetService<IUrlHelperFactory>().GetUrlHelper(this.CustomControllerContext);
        
        public IActionResult DefaultView()
        {
            return new ViewResult();
        }
        
        public IActionResult IndexOutOfRangeException()
        {
            throw new IndexOutOfRangeException();
        }

        public IActionResult CustomActionResult()
        {
            return new CustomActionResult();
        }
        
        public void EmptyAction()
        {
        }
        
        public async Task EmptyActionAsync()
        {
            await Task.Run(() => { });
        }
        
        public IActionResult OkResultAction()
        {
            return new OkResult();
        }
        
        public IActionResult WithRouteData(int id)
        {
            if (this.CustomControllerContext.RouteData.Values["controller"].ToString() == "Mvc"
                && this.CustomControllerContext.RouteData.Values["action"].ToString() == "WithRouteData"
                && this.CustomControllerContext.RouteData.Values["id"].ToString() == "1")
            {
                return new ViewResult();
            }

            throw new InvalidOperationException();
        }
        
        public IActionResult OkResultActionWithRequestBody(int id, RequestModel model)
        {
            return new OkResult();
        }

        public IActionResult WithRequest()
        {
            if (this.CustomHttpContext.Request.Form.Any(f => f.Key == "Test"))
            {
                return new OkResult();
            }

            return new BadRequestResult();
        }

        public IActionResult ModelStateCheck(RequestModel model)
        {
            if (this.CustomControllerContext.ModelState.IsValid)
            {
                return new OkResult();
            }

            return new OkResult();
        }
        
        public async Task<OkResult> AsyncOkResultAction()
        {
            return await Task.Run(() => new OkResult());
        }
        
        [Authorize]
        public IActionResult AuthorizedAction()
        {
            if (this.CustomHttpContext.User.Identity.IsAuthenticated)
            {
                return new OkResult();
            }

            return new NotFoundResult();
        }
        
        public IActionResult UrlAction()
        {
            return new OkObjectResult(this.CustomUrl.Action());
        }
        
        public IActionResult TempDataAction()
        {
            if (this.CustomTempData["test"] != null)
            {
                return new OkResult();
            }

            return new BadRequestResult();
        }

        public IActionResult SessionAction()
        {
            if (this.CustomHttpContext.Session.GetString("test") != null)
            {
                return new OkResult();
            }

            return new BadRequestResult();
        }
    }
}
