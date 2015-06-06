namespace Uri.Grammar
{
    using SLANG;
    using SLANG.Core;

    using Xunit;

    public class SegmentNonZeroLengthNoColonsLexerTests
    {
        [Theory]
        [InlineData(@"@")]
        public void Read_ShouldSucceed(string input)
        {
            var caseInsensitiveTerminalLexerFactory = new CaseInsensitiveTerminalLexerFactory();
            var stringLexerFactory = new StringLexerFactory(caseInsensitiveTerminalLexerFactory);
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var subcomponentsDelimiterLexerFactory = new SubcomponentsDelimiterLexerFactory(
                stringLexerFactory,
                alternativeLexerFactory);
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var sequenceLexerFactory = new SequenceLexerFactory();
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var hexadecimalDigitLexerFactory = new HexadecimalDigitLexerFactory(
                digitLexerFactory,
                stringLexerFactory,
                alternativeLexerFactory);
            var alphaLexerFactory = new AlphaLexerFactory(valueRangeLexerFactory, alternativeLexerFactory);
            var percentEncodingLexerFactory = new PercentEncodingLexerFactory(
                stringLexerFactory,
                hexadecimalDigitLexerFactory,
                sequenceLexerFactory);
            var unreservedLexerFactory = new UnreservedLexerFactory(
                alphaLexerFactory,
                digitLexerFactory,
                stringLexerFactory,
                alternativeLexerFactory);
            var repetitionLexerFactory = new RepetitionLexerFactory();
            var factory = new SegmentNonZeroLengthNoColonsLexerFactory(
                repetitionLexerFactory,
                alternativeLexerFactory,
                unreservedLexerFactory,
                percentEncodingLexerFactory,
                subcomponentsDelimiterLexerFactory,
                stringLexerFactory);
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