﻿namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class WebFramework
    {
        public static class Internals
        {
            public static Type StartupLoader
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Hosting"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Hosting.StartupLoader");
                    return type;
                }
            }

            public static Type ConventionBasedStartup
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Hosting"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Hosting.ConventionBasedStartup");
                    return type;
                }
            }

            public static Type ConfigureContainerBuilder
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Hosting"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Hosting.ConfigureContainerBuilder");
                    return type;
                }
            }

            public static Type RequestCookieCollection
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Http"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Http.RequestCookieCollection");
                    return type;
                }
            }

            public static Type DefaultHttpRequest
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Http"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Http.DefaultHttpRequest");
                    return type;
                }
            }

            public static Type DefaultHttpResponse
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Http"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Http.DefaultHttpResponse");
                    return type;
                }
            }

            public static Type MvcMarkerService
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Core"));
                    var type = assembly.GetType("Microsoft.Extensions.DependencyInjection.MvcMarkerService");
                    return type;
                }
            }

            public static Type TypeActivatorCache
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Core"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Mvc.Infrastructure.ITypeActivatorCache");
                    return type;
                }
            }

            public static Type ObjectMethodExecutor
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Core"));
                    var type = assembly.GetType("Microsoft.Extensions.Internal.ObjectMethodExecutor");
                    return type;
                }
            }

            public static Type AttributeRouting
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Core"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Mvc.Routing.AttributeRouting");
                    return type;
                }
            }

            public static Type MvcAttributeRouteHandler
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Core"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Mvc.Routing.MvcAttributeRouteHandler");
                    return type;
                }
            }

            public static Type ControllerPropertyActivator
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Core"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Mvc.Controllers.IControllerPropertyActivator");
                    return type;
                }
            }

            public static Type ControllerActionInvoker
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Core"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker");
                    return type;
                }
            }

            public static Type ControllerActionInvokerCache
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Core"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvokerCache");
                    return type;
                }
            }

            public static Type ControllerBinderDelegate
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Core"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Mvc.Controllers.ControllerBinderDelegate");
                    return type;
                }
            }

            public static Type ActionMethodExecutor
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Core"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor");
                    return type;
                }
            }

            public static Type ExpressionHelper
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.ViewFeatures"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Mvc.ViewFeatures.ExpressionHelper");
                    return type;
                }
            }

            public static Type LambdaExpressionComparer
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.ViewFeatures"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Mvc.ViewFeatures.LambdaExpressionComparer");
                    return type;
                }
            }

            public static Type IAntiforgeryFeature
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Antiforgery"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Antiforgery.IAntiforgeryFeature");
                    return type;
                }
            }

            public static Type AntiforgeryFeature
            {
                get
                {
                    var assembly = Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Antiforgery"));
                    var type = assembly.GetType("Microsoft.AspNetCore.Antiforgery.AntiforgeryFeature");
                    return type;
                }
            }
        }

        // Copied from the ASP.NET Core source code.
        internal static readonly HashSet<string> AspNetCoreMvcLibraries = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Microsoft.AspNetCore.App",
            "Microsoft.AspNetCore.Mvc",
            "Microsoft.AspNetCore.Mvc.Abstractions",
            "Microsoft.AspNetCore.Mvc.Core",
            "Microsoft.AspNetCore.Mvc.ViewFeatures",
            "Microsoft.AspNetCore.Mvc.ApiExplorer",
            "Microsoft.AspNetCore.Mvc.Cors",
            "Microsoft.AspNetCore.Mvc.DataAnnotations",
            "Microsoft.AspNetCore.Mvc.Formatters.Json",
            "Microsoft.AspNetCore.Mvc.Formatters.Xml",
            "Microsoft.AspNetCore.Mvc.Localization",
            "Microsoft.AspNetCore.Mvc.Razor",
            "Microsoft.AspNetCore.Mvc.RazorPages",
            "Microsoft.AspNetCore.Mvc.TagHelpers"
        };

        internal static string AspNetCoreMetaPackageName => AspNetCoreMvcLibraries.First();
    }
}
