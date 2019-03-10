﻿using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	internal class TitleDoubleWordHyphenatedFirstLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + Title + Space + @"(?<first1>\w+)-(?<first2>\w+)" + Space + @"(?<last>\w+)$",
			CommonPatternRegexOptions);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var firstName1 = match.Groups["first1"].Value;
            var firstName2 = match.Groups["first2"].Value;

            if (!FirstNames.Contains(firstName1.ToLowerInvariant()))
				return null;

            if (!FirstNames.Contains(firstName2.ToLowerInvariant()))
                return null;

            var pn = new ParsedFullName
            {
                Title = match.Groups["title"].Value,
                FirstName = $"{match.Groups["first1"].Value}-{match.Groups["first2"].Value}",
				LastName = match.Groups["last"].Value,
				DisplayName = $"{match.Groups["first1"].Value}-{match.Groups["first2"].Value} {match.Groups["last"].Value}",
				Score = 200
			};
			return pn;
		}
	}
}
