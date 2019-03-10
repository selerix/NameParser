using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    internal class LastNameCommaFirstNameMiddleNamePattern : IFullNamePattern
    {
        private static readonly Regex Rx = new Regex(
            @"^" + Last + CommaSpace + First + OptionalSpace + Middle + @"$",
            CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;
            var firstName = match.Groups["first"].Value;
            var middleName = $"{match.Groups["initial"]}.";
            var lastName = match.Groups["last"].Value;

            var scoreMod = 0;
            ModifyScoreExpectedFirstName(ref scoreMod, firstName);
            ModifyScoreExpectedLastName(ref scoreMod, lastName);

            var pn = new ParsedFullName
            {
                FirstName = match.Groups["first"].Value,
                MiddleName = match.Groups["middle"].Value,
                LastName = match.Groups["last"].Value,
                DisplayName = $"{match.Groups["first"].Value} {match.Groups["middle"].Value} {match.Groups["last"].Value}",
                Score = 250 + scoreMod,
                Rule = nameof(LastNameCommaFirstNameMiddleNamePattern)
            };
            return pn;
        }
    }
}
