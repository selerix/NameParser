﻿using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	public class TwoInitialsFirstLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + TwoInitial + Space + @"(?<last>\w+)$",
			CommonPatternRegexOptions);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var initial1 = match.Groups["initial1"].Value;
			var initial2 = match.Groups["initial2"].Value;
			var firstName = $"{initial1}.";
			var middleName = $"{initial2}.";
			var lastName = match.Groups["last"].Value;


			var scoreMod = 0;

			ModifyScoreExpectedLastName(ref scoreMod, lastName);

			var pn = new ParsedFullName {
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName,
				DisplayName = $"{firstName} {middleName} {lastName}",
				Score = 100 + scoreMod
			};
			return pn;
		}
	}
}