using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    internal class FirstHyphenatedLastSuffixPattern : IFullNamePattern
    {
        private static readonly Regex Rx = new Regex(
            @"^" + First + Space + LastHyphenated + Space + Suffix + @"$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;
            var firstName = match.Groups["first"].Value;
            var lastPart1 = match.Groups["last1"].Value;
            var lastPart2 = match.Groups["last2"].Value;
            var lastName = $"{lastPart1}-{lastPart2}";

            var scoreMod = 0;
            ModifyScoreExpectedFirstName(ref scoreMod, firstName);
            ModifyScoreExpectedLastName(ref scoreMod, lastPart1);
            ModifyScoreExpectedLastName(ref scoreMod, lastPart2);

            var pn = new ParsedFullName
            {
                FirstName = firstName,
                LastName = lastName,
                Suffix = match.Groups["suffix"].Value,
                DisplayName = $"{firstName} {lastName}",
                Score = 100 + scoreMod,
                Rule = nameof(FirstHyphenatedLastSuffixPattern)
            };
            return pn;
        }
    }
}
