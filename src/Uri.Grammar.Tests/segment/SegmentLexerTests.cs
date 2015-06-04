namespace Uri.Grammar.segment
{
    using SLANG;
    using SLANG.Core.ALPHA;
    using SLANG.Core.DIGIT;
    using SLANG.Core.HEXDIG;

    using Uri.Grammar.pchar;
    using Uri.Grammar.pct_encoded;
    using Uri.Grammar.sub_delims;
    using Uri.Grammar.unreserved;

    using Xunit;

    public class SegmentLexerTests
    {
        [Theory]
        [InlineData(@":@")]
        [InlineData(@"@:")]
        public void Read_ShouldSucceed(string input)
        {
            var caseInsensitiveTerminalLexerFactory = new CaseInsensitiveTerminalLexerFactory();
            var stringLexerFactory = new StringLexerFactory(caseInsensitiveTerminalLexerFactory);
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var subcomponentsDelimiterLexerFactory = new SubcomponentsDelimiterLexerFactory(stringLexerFactory, alternativeLexerFactory);
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var sequenceLexerFactory = new SequenceLexerFactory();
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var hexadecimalDigitLexerFactory = new HexadecimalDigitLexerFactory(digitLexerFactory, stringLexerFactory, alternativeLexerFactory);
            var alphaLexerFactory = new AlphaLexerFactory(valueRangeLexerFactory, alternativeLexerFactory);
            var percentEncodingLexerFactory = new PercentEncodingLexerFactory(stringLexerFactory, hexadecimalDigitLexerFactory, sequenceLexerFactory);
            var unreservedLexerFactory = new UnreservedLexerFactory(alphaLexerFactory, digitLexerFactory, stringLexerFactory, alternativeLexerFactory);
            var pathCharacterLexerFactory = new PathCharacterLexerFactory(unreservedLexerFactory, percentEncodingLexerFactory, subcomponentsDelimiterLexerFactory, stringLexerFactory, alternativeLexerFactory);
            var repetitionLexerFactory = new RepetitionLexerFactory();
            var factory = new SegmentLexerFactory(pathCharacterLexerFactory, repetitionLexerFactory);
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
