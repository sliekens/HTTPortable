namespace Uri.Grammar
{
    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    using Xunit;

    public class PercentEncodingLexerTests
    {
        [Theory]
        [InlineData(@"%00")]
        [InlineData(@"%FF")]
        [InlineData(@"%20")]
        [InlineData(@"%99")]
        [InlineData(@"%AA")]
        [InlineData(@"%01")]
        [InlineData(@"%10")]
        public void Read_ShouldSucceed(string input)
        {
            var terminalLexerFactory = new TerminalLexerFactory();
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var concatenationLexerFactory = new ConcatenationLexerFactory();
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var hexadecimalDigitLexerFactory = new HexadecimalDigitLexerFactory(digitLexerFactory, terminalLexerFactory, alternativeLexerFactory);
            var factory = new PercentEncodingLexerFactory(terminalLexerFactory, hexadecimalDigitLexerFactory, concatenationLexerFactory);
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
