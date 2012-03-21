using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine;

namespace Owin.HelloWorld.ViewEngine
{
    public class RazorViewEngine : IViewEngine
    {
        public string ViewFolder { get; set; }

        public RazorViewEngine()
            : this("views")
        {
        }

        public RazorViewEngine(string viewFolder)
        {
            ViewFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, viewFolder);

            if (!Directory.Exists(ViewFolder))
                throw new DirectoryNotFoundException("The view folder specified cannot be located.\r\nThe folder should be in the root of your application which was resolved as " + AppDomain.CurrentDomain.BaseDirectory);
        }

        private FileInfo FindView(string view)
        {
            var file = new FileInfo(Path.Combine(ViewFolder, view + ".cshtml"));
            if (!file.Exists)
                file = new FileInfo(Path.Combine(ViewFolder, view + ".vbhtml"));

            if (!file.Exists)
                throw new FileNotFoundException("No view with the name '" + view + "' was found in the views folder (" + ViewFolder + ").\r\nEnsure that you have a file with that name and an extension of either cshtml or vbhtml");

            return file;
        }

        public string Parse(string viewName)
        {
            return Parse<object>(viewName, null);
        }

        public string Parse<T>(string viewName, T model)
        {
            var file = FindView(viewName);

            return Razor.Parse(File.ReadAllText(file.FullName), model);
        }
    }
}
