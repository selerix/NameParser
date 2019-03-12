using Xunit;

namespace BinaryFog.NameParser.Tests
{
    public class NameParserTest
    {
        private bool GetNameParts(string fullname, out string firstName, out string lastName, out string middleName, out string suffixName)
        {
            firstName = null;
            lastName = null;
            middleName = null;
            suffixName = null;

            if (string.IsNullOrWhiteSpace(fullname))
                return false;

            fullname = fullname.Trim();
            var target = new FullNameParser(fullname);
            target.Parse();
            firstName = target.FirstName;
            lastName = target.LastName;
            middleName = target.MiddleName;
            suffixName = target.Suffix;
            return true;
        }
        private bool GetNameParts(string fullname, out string title, out string firstName, out string lastName, out string middleName, out string suffixName)
        {
            title = null;
            firstName = null;
            lastName = null;
            middleName = null;
            suffixName = null;

            fullname = fullname.Trim();
            if (string.IsNullOrWhiteSpace(fullname))
                return false;

            var target = new FullNameParser(fullname);
            target.Parse();
            title = target.Title;
            firstName = target.FirstName;
            lastName = target.LastName;
            middleName = target.MiddleName;
            suffixName = target.Suffix;
            return true;
        }

        [Fact]
        public void NameParser_EmptyName()
        {
            string fullname = string.Empty;
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.False(ret);
            Assert.Null(firstName);
            Assert.Null(lastName);
            Assert.Null(middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_FirstName_LastName()
        {
            string fullname = "John Doe";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("John", firstName);
            Assert.Equal("Doe", lastName);
            Assert.Null(middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_First_Last_Suffix()
        {
            string fullname = "Michael Crawford III";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Michael", firstName);
            Assert.Equal("Crawford", lastName);
            Assert.Null(middleName);
            Assert.Equal("III", suffixName);
        }


        [Fact]
        public void NameParser_All_Parts()
        {
            string fullname = "Mr Anthony R Von Fange III";
            string title;
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out title, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Mr", title);
            Assert.Equal("Anthony", firstName);
            Assert.Equal("Von Fange", lastName);
            Assert.Equal("R", middleName);
            Assert.Equal("III", suffixName);
        }

        [Fact]
        public void NameParser_Comma_Separated()
        {
            string fullname = "Doe, John";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("John", firstName);
            Assert.Equal("Doe", lastName);
            Assert.Null(middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_Comma_Separated_CompoundLastName()
        {
            string fullname = "O'GRADY, MARY F";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("MARY", firstName);
            Assert.Equal("O'GRADY", lastName);
            Assert.Equal("F", middleName);
            Assert.Null(suffixName);
        }


        [Fact]
        public void NameParser_Full_Middle_Name()
        {
            string fullname = "Mary Catherine Watts";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Mary", firstName);
            Assert.Equal("Watts", lastName);
            Assert.Equal("Catherine", middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_Full_Middle_Name_UpperCase()
        {
            string fullname = "ANNA J. GOSSES";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("ANNA", firstName);
            Assert.Equal("GOSSES", lastName);
            Assert.Equal("J", middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleWithDotLastWithApostrophyHyphenUpperCase()
        {
            string fullname = "MARIE F. O'LEARY-STARK";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("MARIE", firstName);
            Assert.Equal("O'LEARY-STARK", lastName);
            Assert.Equal("F", middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleWithDotLastWithThreePartsUpperCase()
        {
            string fullname = "KIMBERLY S. PONCE DE LEON";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("KIMBERLY", firstName);
            Assert.Equal("PONCE DE LEON", lastName);
            Assert.Equal("S", middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleWithDotLastWithTwoPartsUpperCase()
        {
            string fullname = "ADRIENNE L. RICHARD CASSELBERRY";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("ADRIENNE", firstName);
            Assert.Equal("RICHARD CASSELBERRY", lastName);
            Assert.Equal("L", middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleFullLastWithTwoParts()
        {
            string fullname = "Sabrina Roselea Paskewitz Drew";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Sabrina", firstName);
            Assert.Equal("Paskewitz Drew", lastName);
            Assert.Equal("Roselea", middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleWithDotLastWithHyphen()
        {
            string fullname = "Tammy L. Blythe-Baker";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Tammy", firstName);
            Assert.Equal("Blythe-Baker", lastName);
            Assert.Equal("L", middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_TestFirstLastWithHyphenSuffixDotUpperCase()
        {
            string fullname = "WILLIAM HUGHES-LEWIS JR.";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("WILLIAM", firstName);
            Assert.Equal("HUGHES-LEWIS", lastName);
            Assert.Equal("JR.", suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleWithDotLastWithHyphenSuffixDotUpperCase()
        {
            string fullname = "WILLIAM E. HUGHES-LEWIS JR.";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("WILLIAM", firstName);
            Assert.Equal("HUGHES-LEWIS", lastName);
            Assert.Equal("E", middleName);
            Assert.Equal("JR.", suffixName);
        }

        [Fact]
        public void NameParser_TestFirstLastWithHyphenUpperCase()
        {
            string fullname = "KRYSTAL ALLEN-JACKSON";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("KRYSTAL", firstName);
            Assert.Equal("ALLEN-JACKSON", lastName);
            Assert.Null(middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_TestFirstLastWithHyphen()
        {
            string fullname = "Ananda Illyas-Watson";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Ananda", firstName);
            Assert.Equal("Illyas-Watson", lastName);
            Assert.Null(middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleWithDotLastWithHyphenUpperCase()
        {
            string fullname = "QUN M. HU-YAN";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("QUN", firstName);
            Assert.Equal("HU-YAN", lastName);
            Assert.Equal("M", middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleFullLastWithHyphen()
        {
            string fullname = "Roberto Tomas Ruiz-Flint";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Roberto", firstName);
            Assert.Equal("Ruiz-Flint", lastName);
            Assert.Equal("Tomas", middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleShortDotTwoPartsLast()
        {
            string fullname = "Theodore K.C. Mauch";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Theodore", firstName);
            Assert.Equal("Mauch", lastName);
            Assert.Equal("K C", middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleShortDotSpaceTwoPartsLast()
        {
            string fullname = "Theodore K. C. Mauch";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Theodore", firstName);
            Assert.Equal("Mauch", lastName);
            Assert.Equal("K C", middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleWithDotFirstWithHyphen()
        {
            string fullname = "Tai-Beauty S. Sizemore";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Tai-Beauty", firstName);
            Assert.Equal("Sizemore", lastName);
            Assert.Equal("S", middleName);
            Assert.Null(suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleLastSuffix()
        {
            string fullname = "Jimmy Lee Dabney II";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Jimmy", firstName);
            Assert.Equal("Dabney", lastName);
            Assert.Equal("Lee", middleName);
            Assert.Equal("II", suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleShortLastSuffix()
        {
            string fullname = "William H Moss JR";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("William", firstName);
            Assert.Equal("Moss", lastName);
            Assert.Equal("H", middleName);
            Assert.Equal("JR", suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleShortLastCommaSuffix()
        {
            string fullname = "Brian T. Cureton, Jr.";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Brian", firstName);
            Assert.Equal("Cureton", lastName);
            Assert.Equal("T", middleName);
            Assert.Equal("Jr.", suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleFullLastCommaSuffix()
        {
            string fullname = "Kurt Robert Litterst, Jr.";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Kurt", firstName);
            Assert.Equal("Litterst", lastName);
            Assert.Equal("Robert", middleName);
            Assert.Equal("Jr.", suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleShortLastCommaSuffixUpperCase()
        {
            string fullname = "THOMAS J. DEEP, JR";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("THOMAS", firstName);
            Assert.Equal("DEEP", lastName);
            Assert.Equal("J", middleName);
            Assert.Equal("JR", suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleShortLastCommaUpperCaseSuffixLowerCase()
        {
            string fullname = "GREGORY C. HUTCHINGS, Jr";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("GREGORY", firstName);
            Assert.Equal("HUTCHINGS", lastName);
            Assert.Equal("C", middleName);
            Assert.Equal("Jr", suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleShortLastCommaSuffixDotUpperCase()
        {
            string fullname = "SYLVESTER L. HINDESMILLER, Jr.";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("SYLVESTER", firstName);
            Assert.Equal("HINDESMILLER", lastName);
            Assert.Equal("L", middleName);
            Assert.Equal("Jr.", suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleShortLastSuffixDotUpperCase()
        {
            string fullname = "SYLVESTER L. HINDESMILLER Jr.";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("SYLVESTER", firstName);
            Assert.Equal("HINDESMILLER", lastName);
            Assert.Equal("L", middleName);
            Assert.Equal("Jr.", suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleShortLastCommaSuffixNumber()
        {
            string fullname = "Hamilton F. Biggar, VI";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Hamilton", firstName);
            Assert.Equal("Biggar", lastName);
            Assert.Equal("F", middleName);
            Assert.Equal("VI", suffixName);
        }

        [Fact]
        public void NameParser_TestFirstMiddleShortLastCommaSuffixSir()
        {
            string fullname = "Willie L. Webb, Sr.";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Willie", firstName);
            Assert.Equal("Webb", lastName);
            Assert.Equal("L", middleName);
            Assert.Equal("Sr.", suffixName);
        }

        [Fact]

        public void NameParser_StartWithBlanks()
        {
            string fullname = "   Michael Crawford III";
            string firstName;
            string middleName;
            string lastName;
            string suffixName;

            bool ret = GetNameParts(fullname, out firstName, out lastName, out middleName, out suffixName);

            Assert.True(ret);
            Assert.Equal("Michael", firstName);
            Assert.Equal("Crawford", lastName);
            Assert.Null(middleName);
            Assert.Equal("III", suffixName);
        }

    }
}
