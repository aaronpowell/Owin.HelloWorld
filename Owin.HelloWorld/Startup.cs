using Owin;
using Gate;

namespace Owin.HelloWorld
{
    public class Startup
    {
        public static void Configuration(IAppBuilder builder)
        {
            builder
                .Get((req, res) =>
                {
                    res.ContentType = "text/plain";
                    res.Write("Hello World!\r\n")
                        .End();
                })
                ;
        }
    }

}
