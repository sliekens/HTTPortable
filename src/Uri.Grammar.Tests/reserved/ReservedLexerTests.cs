namespace Uri.Grammar.reserved
{
    using SLANG;

    using Uri.Grammar.gen_delims;
    using Uri.Grammar.sub_delims;

    using Xunit;

    public class ReservedLexerTests
    {
        [Theory]
        [InlineData(@"!")]
        [InlineData(@"$")]
        [InlineData(@"&")]
        [InlineData(@"'")]
        [InlineData(@"(")]
        [InlineData(@")")]
        [InlineData(@"*")]
        [InlineData(@"+")]
        [InlineData(@",")]
        [InlineData(@";")]
        [InlineData(@"=")]
        [InlineData(@"!")]
        [InlineData(@"$")]
        [InlineData(@"&")]
        [InlineData(@"'")]
        [InlineData(@"(")]
        [InlineData(@")")]
        [InlineData(@"*")]
        [InlineData(@"+")]
        [InlineData(@",")]
        [InlineData(@";")]
        [InlineData(@"=")]
        public void Read_ShouldSucceed(string input)
        {
            var caseInsensitiveTerminalLexerFactory = new CaseInsensitiveTerminalLexerFactory();
            var stringLexerFactory = new StringLexerFactory(caseInsensitiveTerminalLexerFactory);
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var subcomponentsDelimiterLexerFactory = new SubcomponentsDelimiterLexerFactory(stringLexerFactory, alternativeLexerFactory);
            var genericDelimiterLexerFactory = new GenericDelimiterLexerFactory(stringLexerFactory, alternativeLexerFactory);
            var factory = new ReservedLexerFactory(genericDelimiterLexerFactory, subcomponentsDelimiterLexerFactory, alternativeLexerFactory);
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var element = lexer.Read(scanner);
                Assert.NotNull(element);
                Assert.Equal(input, element.Data);
            }
        }
    }
}
