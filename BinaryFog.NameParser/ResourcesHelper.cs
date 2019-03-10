using System;
using System.IO;
using System.Reflection;

namespace BinaryFog.NameParser
{
    public static class ResourcesHelper {
		private static Type Type { get; } = typeof(Resources);

		public static string ResourceNamespace { get; } = Type.FullName;
		public static Assembly Assembly { get; } = Type.GetTypeInfo().Assembly;

		private static Stream GetTxtStream(string name) {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            var stream = assembly.GetManifestResourceStream($"{ResourceNamespace}.{name}.txt");
            return stream;

        }

		public static Stream CompanySuffixes { get; }
			= GetTxtStream(nameof(CompanySuffixes));

		public static Stream FemaleFirstNames { get; }
			= GetTxtStream(nameof(FemaleFirstNames));

		public static Stream JobTitles { get; }
			= GetTxtStream(nameof(JobTitles));

		public static Stream LastNamePrefixes { get; }
			= GetTxtStream(nameof(LastNamePrefixes));

		public static Stream MaleFirstNames { get; }
			= GetTxtStream(nameof(MaleFirstNames));

		public static Stream PostNominals { get; }
			= GetTxtStream(nameof(PostNominals));

		public static Stream Suffixes { get; }
			= GetTxtStream(nameof(Suffixes));

		public static Stream Titles { get; }
			= GetTxtStream(nameof(Titles));
		
		public static Stream LastNames { get; }
			= GetTxtStream(nameof(LastNames));

	}
}