﻿using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    public class FirstInitialLastCommaSuffixPattern : IFullNamePattern
    {
        private static readonly Regex Rx = new Regex(
            @"^" + First + Space + Initial + Space + Last + CommaSpace + Suffix + @"$",
            CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;
            var firstName = match.Groups["first"].Value;
            var middleName = $"{match.Groups["initial"]}";
            var lastName = match.Groups["last"].Value;

            var scoreMod = 0;
            ModifyScoreExpectedFirstName(ref scoreMod, firstName);
            ModifyScoreExpectedLastName(ref scoreMod, lastName);

            var pn = new ParsedFullName
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Suffix = match.Groups["suffix"].Value,
                DisplayName = $"{firstName} {middleName} {lastName}",
                Score = 100 + scoreMod,
                Rule = nameof(FirstInitialLastCommaSuffixPattern)
            };
            return pn;
        }
    }
}