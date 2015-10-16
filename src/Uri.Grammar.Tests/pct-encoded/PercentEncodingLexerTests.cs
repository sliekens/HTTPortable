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
            var caseInsensitiveTerminalLexerFactory = new CaseInsensitiveTerminalLexerFactory();
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var sequenceLexerFactory = new SequenceLexerFactory();
            var stringLexerFactory = new StringLexerFactory(caseInsensitiveTerminalLexerFactory);
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var hexadecimalDigitLexerFactory = new HexadecimalDigitLexerFactory(digitLexerFactory, stringLexerFactory, alternativeLexerFactory);
            var factory = new PercentEncodingLexerFactory(stringLexerFactory, hexadecimalDigitLexerFactory, sequenceLexerFactory);
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                scanner.Read();
                var element = lexer.Read(scanner, null);
                Assert.NotNull(element);
                Assert.Equal(input, element.Text);
            }
        }
    }
}
