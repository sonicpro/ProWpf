using System.Linq;
using System.Text.RegularExpressions;

namespace DataTemplate
{
    public class DomainObject
    {
        private int instanceId;

        private static int instanceCounter;

        public string DisplayText
        {
            get
            {
                var className = this.GetType().Name;
                return $"{CamelCaseToHumanReadable(className)} #{instanceId}";
            }
        }

        public DomainObject()
        {
            System.Threading.Interlocked.Increment(ref instanceCounter);
            instanceId = instanceCounter;
        }

        private static string CamelCaseToHumanReadable(string camelCase)
        {
            var camelCaseMatches = Regex.Matches (camelCase, @"[a-z0-9][A-Z]").Cast<Match>();
            var humanReadable = camelCaseMatches.Aggregate(camelCase, (current, match) => {
                return current.Insert(match.Index + 1, " ");
            });
            return humanReadable;
        }
    }
}
