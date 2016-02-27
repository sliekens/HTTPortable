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
            var terminalLexerFactory = new TerminalLexerFactory();
            var factory = new PathEmptyLexerFactory(terminalLexerFactory);
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