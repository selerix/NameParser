using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns
{
    internal class FirstNameOnlyPattern : IFullNamePattern
    {
        private static readonly Regex Rx = new Regex(
            @"^" + First + @"$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);


        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;
            var pn = new ParsedFullName
            {
                DisplayName = rawName,
                Score = 100,
                Rule = nameof(FirstNameOnlyPattern)
            };

            var matchedName = match.Groups["first"].Value;

            if (NameComponentSets.LastNamesInUppercase.Contains(matchedName.ToUpper()))
                pn.LastName = matchedName;
            else
                pn.FirstName = matchedName;

            return pn;
        }
    }
}
