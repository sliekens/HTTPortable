namespace Uri.Grammar
{
    using TextFx;
    using TextFx.ABNF;

    using Xunit;

    public class PathEmptyLexerTests
    {
        [Theory]
        [InlineData(@"")]
        public void Read_ShouldSucceed(string input)
        {
            var caseInsensitiveTerminalLexerFactory = new CaseInsensitiveTerminalLexerFactory();
            var stringLexerFactory = new StringLexerFactory(caseInsensitiveTerminalLexerFactory);
            var factory = new PathEmptyLexerFactory(stringLexerFactory);
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var element = lexer.Read(scanner);
                Assert.NotNull(element);
                Assert.Equal(input, element.Value);
            }
        }
    }
}