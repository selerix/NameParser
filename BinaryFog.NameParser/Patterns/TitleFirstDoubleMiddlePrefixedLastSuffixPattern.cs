using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns {
	internal class TitleFirstDoubleMiddlePrefixedLastSuffixPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + Title + Space + First + Space + @"(?<middle1>\w+)" + Space + @"(?<middle2>\w+)" + Space + Prefix + Space + Last + OptionalCommaSpace + Suffix + @"$",
			CommonPatternRegexOptions);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var prefix = match.Groups["prefix"].Value;
            var pn = new ParsedFullName
            {
                Title = match.Groups["title"].Value,
				FirstName = match.Groups["first"].Value,
				MiddleName = $"{match.Groups["middle1"].Value} {match.Groups["middle2"].Value}",
				LastName = prefix + " " + match.Groups["last"].Value,
				Suffix = match.Groups["suffix"].Value,
				DisplayName = $"{match.Groups["first"].Value} {prefix} {match.Groups["last"].Value}",
				Score = 275
			};
			return pn;
		}
	}
}
