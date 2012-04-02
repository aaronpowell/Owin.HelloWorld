using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin.HelloWorld.ViewEngine
{
    public static class ViewEngineActivator
    {
        private static IViewEngine defaultViewEngine;
        private static Dictionary<string, Tuple<Func<IViewEngine>, IViewEngine>> viewEngines = new Dictionary<string, Tuple<Func<IViewEngine>, IViewEngine>>();

        public static void RegisterViewEngine(string viewEngineId, Func<IViewEngine> viewEngineActivator)
        {
            viewEngines.Add(viewEngineId, new Tuple<Func<IViewEngine>, IViewEngine>(viewEngineActivator, (IViewEngine)null));
        }

        public static IViewEngine ResolveViewEngine(string viewEngineId)
        {
            if (string.IsNullOrEmpty(viewEngineId))
            {
                throw new ArgumentNullException("viewEngineId", "A ViewEngine ID needs to be provided for resolution");
            }

            if (!viewEngines.ContainsKey(viewEngineId))
            {
                throw new KeyNotFoundException(string.Format("The ViewEngine ID {0} has not been registered, ensure it is registered before use", viewEngineId));
            }

            var engine = viewEngines[viewEngineId];

            if (engine.Item2 == null)
            {
                var activator = engine.Item1;
                engine = viewEngines[viewEngineId] = new Tuple<Func<IViewEngine>, IViewEngine>(activator, engine.Item1());
            }

            return engine.Item2;
        }

        public static IViewEngine DefaultViewEngine
        {
            get
            {
                if (defaultViewEngine == null)
                    defaultViewEngine = ResolveViewEngine("defaultViewEngine");

                return defaultViewEngine;
            }
            set
            {
                defaultViewEngine = value;
            }
        }
    }
}
