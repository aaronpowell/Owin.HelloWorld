using Owin;
using Gate;

namespace Owin.HelloWorld
{
    public class Startup
    {
        public static void Configuration(IAppBuilder builder)
        {
            builder
                .RunDirect((req, res) =>
                {
                    res.ContentType = "text/plain";
                    res.Write("Hello World!\r\n")
                        .End();
                })
                ;
        }
    }

}
