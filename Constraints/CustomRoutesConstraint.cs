using System.Globalization;
using System.Text.RegularExpressions;

namespace JobPosting.Constraints
{
    public class CustomRoutesConstraint : IRouteConstraint
    {
        private readonly string _pattern;
        public CustomRoutesConstraint(string pattern)
        {
            _pattern = pattern;
        }
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if(values.TryGetValue(routeKey, out var routeValue))
            {
                var valueString = Convert.ToString(routeValue, System.Globalization.CultureInfo.InvariantCulture);
                if(!string.IsNullOrEmpty(valueString) && Regex.IsMatch(valueString,_pattern)) {
                    return true;
                }
                
            }
            return false;
        }
    }
}
