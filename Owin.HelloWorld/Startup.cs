using Owin;
using Owin.HelloWorld.Routing;
using Gate;

namespace Owin.HelloWorld
{
    public class Startup
    {
        public static void Configuration(IAppBuilder builder)
        {
            builder
                .Get("/hi", (req, res) =>
                {
                    res.ContentType = "text/plain";
                    res.End("Oh hi there, I'm a different route\r\n");
                })
                .Get(@"/users/(?<id>\d{1,5})/subscribed/:email", (req, res) =>
                {
                    res.ContentType = "text/plain";
                    res.End("Email " + req.UrlSegments.email + " is subscribed.\r\n");
                })
                .Delete(@"/users/(?<id>\d{1,5})/unsubscribe/:email", (req, res) =>
                {
                    res.ContentType = "text/plain";
                    res.End("Email " + req.UrlSegments.email + " has been unsubscribed.\r\n");
                })
                .Get("/json", (req, res) =>
                {
                    res.Json(new { FirstName = "Aaron", LastName = "Powell" });
                })
                .Get("/json/:name", (req, res) =>
                {
                    res.Json(new { Name = req.UrlSegments.name });
                })
                .Get("/razor/basic", (req, res) =>
                {
                    res.View("Basic");
                })
                .Get("/razor/model/:name", (req, res) =>
                {
                    res.View("Model", new { Name = req.UrlSegments.name });
                })
                .Get((req, res) =>
                {
                    res.Text("Well nothing else matched, that's sad :(\r\n");
                })
                ;
        }
    }

}
