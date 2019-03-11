using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    public class FirstInitialTrippleLastPattern : IFullNamePattern
    {
        private static readonly Regex Rx = new Regex(
            @"^" + First + Space + Initial + Space + @"(?<last1>\w+) (?<last2>\w+) (?<last3>\w+)$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;

            var pn = new ParsedFullName
            {
                FirstName = $"{match.Groups["first"].Value}",
                LastName = $"{match.Groups["last1"].Value} {match.Groups["last2"].Value} {match.Groups["last3"].Value}",
                MiddleName = $"{match.Groups["initial"].Value}",
                DisplayName = $"{match.Groups["first"].Value} {match.Groups["initial"].Value}. {match.Groups["last1"].Value} {match.Groups["last2"].Value} {match.Groups["last3"].Value}",
                Score = 9,
                Rule = nameof(FirstInitialTrippleLastPattern)
            };
            return pn;
        }
    }
}