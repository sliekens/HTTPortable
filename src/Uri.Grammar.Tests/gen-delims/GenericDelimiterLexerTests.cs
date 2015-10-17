namespace Uri.Grammar
{
    using TextFx;
    using TextFx.ABNF;

    using Xunit;

    public class GenericDelimiterLexerTests
    {
        [Theory]
        [InlineData(@":")]
        [InlineData(@"/")]
        [InlineData(@"?")]
        [InlineData(@"#")]
        [InlineData(@"[")]
        [InlineData(@"]")]
        [InlineData(@"@")]
        public void Read_ShouldSucceed(string input)
        {
            var terminalLexerFactory = new TerminalLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var factory = new GenericDelimiterLexerFactory(terminalLexerFactory, alternativeLexerFactory);
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner, null);
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}
