using System;
using System.IO;
using Gate;
using RazorEngine;

namespace Owin.HelloWorld
{
    public static class ViewEngine
    {
        private static FileInfo FindView(string view)
        {
            if (string.IsNullOrEmpty(view))
                throw new ArgumentNullException("view", "View name is required");

            var root = AppDomain.CurrentDomain.BaseDirectory;

            var viewsPath = Path.Combine(root, "views");

            var file = new FileInfo(Path.Combine(viewsPath, view + ".cshtml"));
            if (!file.Exists)
                file = new FileInfo(Path.Combine(viewsPath, view + ".vbhtml"));

            if (!file.Exists)
                throw new FileNotFoundException("No view with the name '" + view + "' was found in the views folder (" + viewsPath + ").\r\nEnsure that you have a file with that name and an extension of either cshtml or vbhtml");

            return file;
        }

        public static void View(this Response res, string view)
        {
            var file = FindView(view);

            var raw = File.ReadAllText(file.FullName);

            var output = Razor.Parse(raw);

            res.ContentType = "text/html";
            res.Status = "200 OK";
            res.End(output);
        }

        public static void View<T>(this Response res, string view, T model)
        {
            var file = FindView(view);

            var raw = File.ReadAllText(file.FullName);

            var output = Razor.Parse(raw, model);

            res.ContentType = "text/html";
            res.Status = "200 OK";
            res.End(output);
        }
    }
}
