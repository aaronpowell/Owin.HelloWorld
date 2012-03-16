using System;
using System.Linq;
using System.Text.RegularExpressions;
namespace Owin.HelloWorld.Routing
{
    internal static class RouteBuilder
    {
        private static readonly Regex paramRegex = new Regex(@":(?<name>[A-Za-z0-9_]*)", RegexOptions.Compiled);

        internal static Regex RouteToRegex(string route)
        {
            var parts = route.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries).AsEnumerable();

            parts = parts.Select(part => !paramRegex.IsMatch(part) ?
                part :
                string.Join("",
                    paramRegex.Matches(part)
                        .Cast<Match>()
                        .Where(match => match.Success)
                        .Select(match => string.Format(
                            "(?<{0}>.+?)",
                            match.Groups["name"].Value.Replace(".", @"\.")
                            )
                        )
                    )
                );

            return new Regex("^/" + string.Join("/", parts) + "$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
    }
}
