namespace Uri.Grammar
{
    using SLANG;

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
            var stringLexerFactory = new StringLexerFactory(caseInsensitiveTerminalLexerFactory);
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var factory = new GenericDelimiterLexerFactory(stringLexerFactory, alternativeLexerFactory);
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var element = lexer.Read(scanner);
                Assert.NotNull(element);
                Assert.Equal(input, element.Values);
            }
        }
    }
}
