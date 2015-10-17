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
            var caseInsensitiveTerminalLexerFactory = new CaseInsensitiveTerminalLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var factory = new GenericDelimiterLexerFactory(caseInsensitiveTerminalLexerFactory, alternativeLexerFactory);
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var element = lexer.Read(scanner, null);
                Assert.NotNull(element);
                Assert.Equal(input, element.Text);
            }
        }
    }
}
