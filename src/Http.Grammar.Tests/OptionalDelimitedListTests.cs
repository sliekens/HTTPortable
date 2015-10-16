namespace Http.Grammar.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    using Xunit;

    public class OptionalDelimitedListTests
    {
        public static IEnumerable<object[]> GetTestData()
        {
            var sl = new StringLexerFactory(new CaseInsensitiveTerminalLexerFactory());
            var listItemLexer = new AlternativeLexer(sl.Create("foo"), sl.Create("bar"), sl.Create("charlie"));
            yield return new object[] { "foo,bar", "foo, bar", listItemLexer };

            yield return new object[] { "foo ,bar,", "foo, bar", listItemLexer };

            yield return new object[] { "foo , ,bar,charlie   ", "foo, bar, charlie", listItemLexer };
        }

        [Theory]
        [MemberData("GetTestData")]
        public void GetWellFormedText_NormalizesWhiteSpace(string input, string expected, ILexer listItemLexer)
        {
            var optionalDelimitedList = CreateTestCase(input, listItemLexer);
            var result = optionalDelimitedList.GetWellFormedText();
            Assert.Equal(expected, result);
        }

        private static OptionalDelimitedList CreateTestCase(string input, ILexer listItemLexer)
        {
            var optionLexerFactory = new OptionLexerFactory();
            var sequenceLexerFactory = new SequenceLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var caseInsensitiveTerminalLexerFactory = new CaseInsensitiveTerminalLexerFactory();
            var stringLexerFactory = new StringLexerFactory(caseInsensitiveTerminalLexerFactory);
            var spaceLexerFactory = new SpaceLexerFactory(caseInsensitiveTerminalLexerFactory);
            var repetitionLexerFactory = new RepetitionLexerFactory();
            var horizontalTabLexerFactory = new HorizontalTabLexerFactory(caseInsensitiveTerminalLexerFactory);
            var whiteSpaceLexerFactory = new WhiteSpaceLexerFactory(spaceLexerFactory, horizontalTabLexerFactory, alternativeLexerFactory);
            var optionalWhiteSpaceLexerFactory = new OptionalWhiteSpaceLexerFactory(repetitionLexerFactory, whiteSpaceLexerFactory);
            var lexerFactory = new OptionalDelimitedListLexerFactory(
                optionLexerFactory,
                sequenceLexerFactory,
                alternativeLexerFactory,
                stringLexerFactory,
                optionalWhiteSpaceLexerFactory,
                repetitionLexerFactory);

            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                scanner.Read();
                return lexerFactory.Create(listItemLexer).Read(scanner, null);
            }
        }
    }
}