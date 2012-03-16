using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Gate;
namespace Owin.HelloWorld.Routing
{
    public static class Routing
    {
        private static AppDelegate ProcessRequest(AppDelegate next, Regex regex, string method, Action<RoutedRequest, Response> app)
        {
            return (env, result, fault) =>
            {
                var path = (string)env["owin.RequestPath"];

                if (path.EndsWith("/"))
                    path = path.TrimEnd('/');

                if ((string)env["owin.RequestMethod"] == method && regex.IsMatch(path))
                {
                    var req = new RoutedRequest(env, regex, path);
                    var res = new Response(result);
                    app(req, res);
                }
                else
                {
                    next(env, result, fault);
                }
            };
        }

        public static IAppBuilder Get(this IAppBuilder builder, string route, Action<RoutedRequest, Response> app)
        {
            var regex = RouteBuilder.RouteToRegex(route);

            return builder.Use<AppDelegate>(next => ProcessRequest(next, regex, "GET", app));
        }

        public static IAppBuilder Post(this IAppBuilder builder, string route, Action<RoutedRequest, Response> app)
        {
            var regex = RouteBuilder.RouteToRegex(route);

            return builder.Use<AppDelegate>(next => ProcessRequest(next, regex, "POST", app));
        }

        public static IAppBuilder Put(this IAppBuilder builder, string route, Action<RoutedRequest, Response> app)
        {
            var regex = RouteBuilder.RouteToRegex(route);

            return builder.Use<AppDelegate>(next => ProcessRequest(next, regex, "PUT", app));
        }

        public static IAppBuilder Delete(this IAppBuilder builder, string route, Action<RoutedRequest, Response> app)
        {
            var regex = RouteBuilder.RouteToRegex(route);

            return builder.Use<AppDelegate>(next => ProcessRequest(next, regex, "DELETE", app));
        }

        public static IAppBuilder Patch(this IAppBuilder builder, string route, Action<RoutedRequest, Response> app)
        {
            var regex = RouteBuilder.RouteToRegex(route);

            return builder.Use<AppDelegate>(next => ProcessRequest(next, regex, "PATCH", app));
        }
    }
}
