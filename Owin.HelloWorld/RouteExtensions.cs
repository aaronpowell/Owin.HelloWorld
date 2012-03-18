using Gate;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Owin.HelloWorld
{
    public static class RouteExtensions
    {
        public static void Json(this Response res, dynamic obj, bool useJavaScriptNaming = true)
        {
            res.ContentType = "application/json";
            res.Status = "200 OK";

            var serializer = new JsonSerializer();

            if (useJavaScriptNaming)
                serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

            res.End(JObject.FromObject(obj, serializer).ToString());
        }

        public static void Text(this Response res, string text)
        {
            res.ContentType = "text/plain";
            res.Status = "200 OK";
            res.End(text);
        }
    }
}
