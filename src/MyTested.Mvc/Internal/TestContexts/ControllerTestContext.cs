namespace MyTested.Mvc.Internal.TestContexts
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    public class ControllerTestContext : HttpTestContext
    {
        private IEnumerable<object> controllerAttributes;
        private string actionName;
        private MethodInfo action;
        private IEnumerable<object> actionAttributes;
        private object model;
        
        public object Controller { get; internal set; }

        public IEnumerable<object> ControllerAttributes
        {
            get
            {
                if (this.controllerAttributes == null)
                {
                    this.controllerAttributes = Reflection.GetCustomAttributes(this.Controller);
                }

                return this.controllerAttributes;
            }
        }

        public string ActionName
        {
            get
            {
                return this.actionName;
            }

            internal set
            {
                CommonValidator.CheckForNotWhiteSpaceString(value, nameof(ActionName));
                this.actionName = value;
            }
        }

        public MethodInfo Action
        {
            get
            {
                return this.action;
            }

            internal set
            {
                CommonValidator.CheckForNullReference(value, nameof(Action));
                this.action = value;
            }
        }

        public object ActionResult { get; internal set; }

        public IEnumerable<object> ActionAttributes
        {
            get
            {
                if (this.actionAttributes == null)
                {
                    this.actionAttributes = Reflection.GetCustomAttributes(this.Action);
                }

                return this.actionAttributes;
            }
        }

        public Exception CaughtException { get; internal set; }

        public object Model
        {
            get
            {
                if (this.model == null)
                {
                    return this.ActionResult;
                }

                return this.model;
            }

            internal set
            {
                this.model = value;
            }
        }
        
        internal TController ControllerAs<TController>()
            where TController : class => this.Controller as TController;

        internal TActionResult ActionResultAs<TActionResult>() => this.ActionResult.TryCastTo<TActionResult>();

        internal TException CaughtExceptionAs<TException>()
            where TException : Exception => this.CaughtException as TException;

        internal TModel ModelAs<TModel>() => this.Model.TryCastTo<TModel>();

        internal void Apply<TActionResult>(TestActionDescriptor<TActionResult> testActionDescriptor)
        {
            this.ActionName = testActionDescriptor.ActionName;
            this.Action = testActionDescriptor.Action;
            this.ActionResult = testActionDescriptor.ActionResult;
            this.CaughtException = testActionDescriptor.CaughtException;
        }
    }
}
