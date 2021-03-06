﻿using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    public class FirstTwoMiddleLastPattern : IFullNamePattern
    {
        private static readonly Regex Rx = new Regex(
            @"^" + First + Space + TwoMiddle + Space + Last + @"$",
            CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;

            var firstName = match.Groups["first"].Value;
            var middleName1 = match.Groups["middle1"].Value;
            var middleName2 = match.Groups["middle2"].Value;
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
                Score = 50 + scoreMod,
                Rule = nameof(FirstTwoMiddleLastPattern)
            };
            return pn;
        }
    }
}
