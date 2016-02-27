namespace Uri.Grammar
{
    using TextFx;
    using TextFx.ABNF;

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
            var terminalLexerFactory = new TerminalLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var subcomponentsDelimiterLexerFactory = new SubcomponentsDelimiterLexerFactory(terminalLexerFactory, alternativeLexerFactory);
            var genericDelimiterLexerFactory = new GenericDelimiterLexerFactory(terminalLexerFactory, alternativeLexerFactory);
            var factory = new ReservedLexerFactory(genericDelimiterLexerFactory, subcomponentsDelimiterLexerFactory, alternativeLexerFactory);
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner);
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}
