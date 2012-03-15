using System;
using Firefly.Http;
using Gate.Builder;
using Owin;

namespace Owin.HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new AppBuilder().Build(Startup.Configuration);

            var server = new ServerFactory().Create(app, 1337);

            Console.WriteLine("Running on http://localhost:1337");

            Console.ReadLine();
        }
    }
}
