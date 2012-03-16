using System;
using Gate;
using Owin;
namespace Owin.HelloWorld.Routing
{
    public static class Middleware
    {
        public static IAppBuilder Get(this IAppBuilder builder, Action<Request, Response> app)
        {
            return builder.Use<AppDelegate>(next => (env, result, fault) =>
            {
                if ((string)env["owin.RequestMethod"] == "GET")
                {
                    var req = new Request(env);
                    var res = new Response(result);
                    app(req, res);

                }
                else
                {
                    next(env, result, fault);
                }
            });
        }

        public static IAppBuilder Post(this IAppBuilder builder, Action<Request, Response> app)
        {
            return builder.Use<AppDelegate>(next => (env, result, fault) =>
            {
                if ((string)env["owin.RequestMethod"] == "POST")
                {
                    var req = new Request(env);
                    var res = new Response(result);
                    app(req, res);

                }
                else
                {
                    next(env, result, fault);
                }
            });
        }

        public static IAppBuilder Put(this IAppBuilder builder, Action<Request, Response> app)
        {
            return builder.Use<AppDelegate>(next => (env, result, fault) =>
            {
                if ((string)env["owin.RequestMethod"] == "PUT")
                {
                    var req = new Request(env);
                    var res = new Response(result);
                    app(req, res);

                }
                else
                {
                    next(env, result, fault);
                }
            });
        }

        public static IAppBuilder Delete(this IAppBuilder builder, Action<Request, Response> app)
        {
            return builder.Use<AppDelegate>(next => (env, result, fault) =>
            {
                if ((string)env["owin.RequestMethod"] == "DELETE")
                {
                    var req = new Request(env);
                    var res = new Response(result);
                    app(req, res);

                }
                else
                {
                    next(env, result, fault);
                }
            });
        }

        public static IAppBuilder Patch(this IAppBuilder builder, Action<Request, Response> app)
        {
            return builder.Use<AppDelegate>(next => (env, result, fault) =>
            {
                if ((string)env["owin.RequestMethod"] == "PATCH")
                {
                    var req = new Request(env);
                    var res = new Response(result);
                    app(req, res);

                }
                else
                {
                    next(env, result, fault);
                }
            });
        }
    }
}
