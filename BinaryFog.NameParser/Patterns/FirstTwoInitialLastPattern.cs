using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    class FirstTwoInitialLastPattern : IFullNamePattern
    {
        private static readonly Regex Rx = new Regex(
            @"^" + First + Space + TwoInitial + Space + Last + @"$",
            CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;

            var firstName = match.Groups["first"].Value;
            var middleName1 = match.Groups["initial1"].Value;
            var middleName2 = match.Groups["initial2"].Value;
            var lastName = match.Groups["last"].Value;

            var scoreMod = 0;
            ModifyScoreExpectedFirstNames(ref scoreMod, firstName, middleName1, middleName2);
            ModifyScoreExpectedLastName(ref scoreMod, lastName);

            var middleName = $"{middleName1} {middleName2}";
            var pn = new ParsedFullName
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                DisplayName = $"{firstName} {middleName} {lastName}",
                Score = 60 + scoreMod,
                Rule = nameof(FirstTwoInitialLastPattern)
            };
            return pn;
        }
    }
}