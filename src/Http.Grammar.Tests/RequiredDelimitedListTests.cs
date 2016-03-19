namespace Http.Grammar.Tests
{
    using System;
    using System.Collections.Generic;
    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    using Xunit;

    public class RequiredDelimitedListTests
    {
        public static IEnumerable<object[]> GetTestData()
        {
            var factory = new TerminalLexerFactory();
            var foo = factory.Create("foo", StringComparer.OrdinalIgnoreCase);
            var bar = factory.Create("bar", StringComparer.OrdinalIgnoreCase);
            var charlie = factory.Create("charlie", StringComparer.OrdinalIgnoreCase);
            var listItemLexer = new AlternativeLexer(foo, bar, charlie);
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

        private static RequiredDelimitedList CreateTestCase(string input, ILexer listItemLexer)
        {
            var optionLexerFactory = new OptionLexerFactory();
            var concatenationLexerFactory = new ConcatenationLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var terminalLexerFactory = new TerminalLexerFactory();
            var spaceLexerFactory = new SpaceLexerFactory(terminalLexerFactory);
            var repetitionLexerFactory = new RepetitionLexerFactory();
            var horizontalTabLexerFactory = new HorizontalTabLexerFactory(terminalLexerFactory);
            var whiteSpaceLexerFactory = new WhiteSpaceLexerFactory(spaceLexerFactory, horizontalTabLexerFactory, alternativeLexerFactory);
            var optionalWhiteSpaceLexerFactory = new OptionalWhiteSpaceLexerFactory(repetitionLexerFactory, whiteSpaceLexerFactory);
            var lexerFactory = new RequiredDelimitedListLexerFactory(
                repetitionLexerFactory,
                concatenationLexerFactory,
                optionLexerFactory,
                terminalLexerFactory,
                optionalWhiteSpaceLexerFactory);
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                return lexerFactory.Create(listItemLexer).Read(scanner).Element;
            }
        }
    }
}
