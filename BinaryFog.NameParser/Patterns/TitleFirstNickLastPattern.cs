﻿using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	internal class TitleFirstNickLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + Title + Space + First + OptionalSpace + Nick + OptionalSpace + Last + @"$",
			CommonPatternRegexOptions);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var firstName = match.Groups["first"].Value;
			var lastName = match.Groups["last"].Value;
			var nickName = match.Groups["nick"].Value;
			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedName(ref scoreMod, nickName);
			ModifyScoreExpectedLastName(ref scoreMod, lastName);
			var pn = new ParsedFullName {
                Title = match.Groups["title"].Value,
				FirstName = firstName,
				LastName = lastName,
				NickName = nickName,
				DisplayName = $"{firstName} {lastName}",
				Score = 100 + scoreMod
			};
			return pn;
		}
	}
}
