using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BinaryFog.NameParser
{
    using static Helpers;
	/// <summary>
	/// Parse a person full name 
	/// </summary>
	/// <example>
	/// 1. Mr Jack Johnson  => Title = "Mr", First Name = "Jack" Last Name = "Johnson"
	/// 2. Jack Johnson  => First Name = "Jack" Last Name = "Johnson"
	/// 3. Jack => First Name = "Jack"
	/// 4. Jack Johnson Enterprises => ignored
	/// 5. Pasquale (Pat) Vacoturo  =>  First Name = "Pasquale" Last Name = "Vacoturo" Nickname = Pat 
	/// 6. Mr Giovanni Van Der Hutte  => Title = "Mr", First Name = "Giovanni" Last Name = "Van Der Hutte"
	/// 7. Giovanni Van Der Hutte  => First Name = "Giovanni" Last Name = "Van Der Hutte"
	/// </example>
	/// <remarks>
	/// 1. The prefix "ATTN:" is removed if exists and the parsing proceeds on the new string
	/// </remarks>
	public class FullNameParser {
		public IReadOnlyList<ParsedFullName> Results { get; set; } = new ParsedFullName[0];

        protected string FullName { get; private set; }

		protected static Type PatternType { get; } = typeof(IFullNamePattern);

        private static readonly IEnumerable<IFullNamePattern> PatternsMap =
            //AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => p.IsClass && PatternType.IsAssignableFrom(p))
            //.Select(t => t.GetConstructor(Type.EmptyTypes)?.Invoke(null)).OfType<IFullNamePattern>().Where(o => o != null);
            new IFullNamePattern[]
            {
                new Patterns.AfricanFirstLastPattern(),
                new Patterns.CompanyPattern(),
                //new Patterns.DoubleInitialsFirstLastPattern(),
                //new Patterns.DoubleWordFirstLastPattern(),
                //new Patterns.DoubleWordHyphenatedFirstLastPattern(),
                //new Patterns.FirstDoubleWordHyphenatedLastPattern(),
                //new Patterns.FirstDoubleWordLastPattern(),
                new Patterns.FirstHyphenatedLastNickPattern(),
                new Patterns.FirstHyphenatedLastPattern(),
                new Patterns.FirstInitialHyphenatedLastPattern(),
                new Patterns.FirstInitialHyphenatedLastSuffixPattern(),
                new Patterns.FirstInitialLastCommaSuffixPattern(),
                new Patterns.FirstInitialLastPattern(),
                new Patterns.FirstInitialLastSuffixPattern(),
                new Patterns.FirstInitialPrefixedLastPattern(),
                new Patterns.FirstInitialPrefixedLastSuffixPattern(),
                new Patterns.FirstIrishLastPattern(),
                new Patterns.FirstLastNickPattern(),
                new Patterns.FirstLastPattern(),
                new Patterns.FirstLastSuffixPattern(),
                new Patterns.FirstMiddleHyphenatedLastPattern(),
                new Patterns.FirstMiddleHyphenatedLastSuffixPattern(),
                new Patterns.FirstMiddleLastPattern(),
                new Patterns.FirstMiddleLastSuffixPattern(),
                new Patterns.FirstMiddlePrefixedLastPattern(),
                new Patterns.FirstMiddlePrefixedLastSuffixPattern(),
                new Patterns.FirstMiddleTwoLastPattern(),
                new Patterns.FirstNameJobTitlePattern(),
//                new Patterns.FirstNameOnlyPattern(),
                new Patterns.FirstNickHyphenatedLastPattern(),
                new Patterns.FirstNickInitialLastPattern(),
                new Patterns.FirstNickLastPattern(),
                new Patterns.FirstNickMiddleHyphenatedLastPattern(),
                new Patterns.FirstNickMiddleLastPattern(),
                new Patterns.FirstPrefixedLastPattern(),
                new Patterns.FirstPrefixedLastSuffixPattern(),
                new Patterns.FirstTwoLastPattern(),
                new Patterns.FirstTwoMiddleHyphenatedLastPattern(),
                new Patterns.FirstTwoMiddleLastPattern(),
                new Patterns.HyphenatedFirstLastPattern(),
                new Patterns.LastNameCommaFirstNameInitialPattern(),
                new Patterns.LastNameCommaFirstNameMiddleNamePattern(),
                new Patterns.LastNameCommaFirstNameNickNameInitialPattern(),
                new Patterns.LastNameCommaFirstNamePattern(),
                new Patterns.SingleHyphenatedNameOnlyPattern(),
                new Patterns.SingleNameOnlyPattern(),
                new Patterns.TitleDoubleWordHyphenatedFirstLastPattern(),
                new Patterns.TitleFirstDoubleMiddlePrefixedLastSuffixPattern(),
                new Patterns.TitleFirstDoubleWordHyphenatedLastPattern(),
                new Patterns.TitleFirstDoubleWordLastPattern(),
                new Patterns.TitleFirstHyphenatedLastPattern(),
                new Patterns.TitleFirstInitialLastPattern(),
                new Patterns.TitleFirstInitialLastSuffixPattern(),
                new Patterns.TitleFirstInitialPrefixedLastSuffixPattern(),
                new Patterns.TitleFirstIrishLastPattern(),
                new Patterns.TitleFirstLastPattern(),
                new Patterns.TitleFirstLastSuffixPattern(),
                new Patterns.TitleFirstMiddlePrefixedLastSuffixPattern(),
                new Patterns.TitleFirstNameJobTitlePattern(),
                new Patterns.TitleFirstNickLastPattern(),
                new Patterns.TitleFirstNickLastSuffixPattern(),
                new Patterns.TitleFirstPrefixedLastPattern(),
                new Patterns.TitleFirstTwoLastPattern(),
                new Patterns.TitleFirstTwoMiddlePrefixedLastSuffixPattern(),
                new Patterns.TitleHyphenatedFirstLastPattern(),
                new Patterns.TwoInitialsFirstLastPattern(),
                new Patterns.TwoInitialsFirstLastSuffixPattern(),
            };


        public string FirstName { get; private set; }
		public string MiddleName { get; private set; }
		public string LastName { get; private set; }
		public string Title { get; private set; }
		public string NickName { get; private set; }
		public string Suffix { get; private set; }
		public string DisplayName { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="FullNameParser"/> class.
		/// </summary>
		/// <param name="fullName">The full name.</param>
		public FullNameParser(string fullName) {
			FullName = fullName;
		}

		public static FullNameParser Parse(string fullName) {
			var name = new FullNameParser(fullName);
			name.Parse();
			return name;
		}

		/// <summary>
		/// Parses this instance.
		/// </summary>
		public void Parse() {
			DisplayName = FullName;
			if (string.IsNullOrWhiteSpace(FullName))
				return;

			Preparse();

			Results = PatternsMap
				.Select(pattern => pattern.Parse(FullName))
				.Where(NotNull)
				.OrderByDescending( result => result?.Score ?? 0 )
				.ToImmutableArray();

			var selectedResult = Results.FirstOrDefault();

			FirstName = selectedResult?.FirstName;
			MiddleName = selectedResult?.MiddleName;
			LastName = selectedResult?.LastName;
			Title = selectedResult?.Title;
			NickName = selectedResult?.NickName;
			Suffix = selectedResult?.Suffix;
			DisplayName = selectedResult?.DisplayName ?? FullName;

		}

		/// <summary>
		/// Removes the attn prefix if needed.
		/// </summary>
		protected void Preparse() {
			if (FullName.StartsWith("ATTN:", StringComparison.OrdinalIgnoreCase))
				FullName = FullName.Substring(5).Trim();
			}
}
}
