namespace Uri.Grammar
{
    using SLANG;
    using SLANG.Core.DIGIT;
    using SLANG.Core.HEXDIG;

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
