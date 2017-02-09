using System;
using System.Collections.Generic;
using System.Linq;
using Txt;
using Txt.ABNF;
using Txt.Core;
using Xunit;

namespace Http
{
    public class OptionalDelimitedListTests
    {
        public static IEnumerable<object[]> GetTestData()
        {
            var factory = new TerminalLexerFactory();
            var comparer = StringComparer.OrdinalIgnoreCase;
            var foo = factory.Create("foo", comparer);
            var bar = factory.Create("bar", comparer);
            var charlie = factory.Create("charlie", comparer);
            var listItemLexer = new AlternationLexer(foo, bar, charlie);
            yield return new object[] { "foo,bar", "foo, bar", listItemLexer };
            yield return new object[] { "foo ,bar,", "foo, bar", listItemLexer };
            yield return new object[] { "foo , ,bar,charlie   ", "foo, bar, charlie", listItemLexer };
        }

        [Theory]
        [MemberData("GetTestData")]
        public void GetWellFormedText_NormalizesWhiteSpace(string input, string expected, ILexer<Element> listItemLexer)
        {
            var optionalDelimitedList = CreateTestCase(input, listItemLexer);
            var result = string.Join(", ", optionalDelimitedList.GetItems().Select(o => o.Text));
            Assert.Equal(expected, result);
        }

        private static OptionalDelimitedList CreateTestCase(string input, ILexer<Element> listItemLexer)
        {
            var lexerFactory = OptionalDelimitedListLexerFactory.Default;
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                return lexerFactory.Create(listItemLexer).Read(scanner);
            }
        }
    }
}
