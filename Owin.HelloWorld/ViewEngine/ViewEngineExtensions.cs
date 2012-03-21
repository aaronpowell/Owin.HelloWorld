using System;
using System.IO;
using Gate;
using RazorEngine;

namespace Owin.HelloWorld.ViewEngine
{
    public static class ViewEngineExtensions
    {
        public static IAppBuilder UseViewEngine<TViewEngine>(this IAppBuilder builder)
            where TViewEngine : IViewEngine, new()
        {
            return builder.UseViewEngine(new TViewEngine());
        }

        public static IAppBuilder UseViewEngine<TViewEngine>(this IAppBuilder builder, TViewEngine viewEngine)
            where TViewEngine : IViewEngine
        {
            ViewEngineActivator.ViewEngine = viewEngine;
            return builder;
        }

        public static void View(this Response res, string view)
        {
            var output = ViewEngineActivator.ViewEngine.Parse(view);

            res.ContentType = "text/html";
            res.Status = "200 OK";
            res.End(output);
        }

        public static void View<T>(this Response res, string view, T model)
        {
            var output = ViewEngineActivator.ViewEngine.Parse(view, model);

            res.ContentType = "text/html";
            res.Status = "200 OK";
            res.End(output);
        }
    }
}
