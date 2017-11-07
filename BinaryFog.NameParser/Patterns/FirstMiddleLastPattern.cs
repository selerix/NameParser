using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns
{
    internal class FirstMiddleLastPattern : IPattern
    {
        private static readonly Regex Rx = new Regex(
            @"^" + First + Space + Middle + Space + Last + @"$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public ParsedName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;

            var pn = new ParsedName(this.GetType().Name)
            {
                FirstName = match.Groups["first"].Value,
                MiddleName = match.Groups["middle"].Value,
                LastName = match.Groups["last"].Value,
                DisplayName = $"{match.Groups["first"].Value} {match.Groups["middle"].Value} {match.Groups["last"].Value}",
                Score = 199
            };
            return pn;
        }
    }
}
