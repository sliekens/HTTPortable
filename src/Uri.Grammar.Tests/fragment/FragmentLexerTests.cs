namespace Uri.Grammar
{
    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    using Xunit;

    public class FragmentLexerTests
    {
        [Theory]
        [InlineData(@":")]
        [InlineData(@"@")]
        [InlineData(@"/")]
        [InlineData(@"?")]
        public void Read_ShouldSucceed(string input)
        {
            var caseInsensitiveTerminalLexerFactory = new CaseInsensitiveTerminalLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var subcomponentsDelimiterLexerFactory = new SubcomponentsDelimiterLexerFactory(caseInsensitiveTerminalLexerFactory, alternativeLexerFactory);
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var sequenceLexerFactory = new SequenceLexerFactory();
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var hexadecimalDigitLexerFactory = new HexadecimalDigitLexerFactory(digitLexerFactory, caseInsensitiveTerminalLexerFactory, alternativeLexerFactory);
            var alphaLexerFactory = new AlphaLexerFactory(valueRangeLexerFactory, alternativeLexerFactory);
            var percentEncodingLexerFactory = new PercentEncodingLexerFactory(caseInsensitiveTerminalLexerFactory, hexadecimalDigitLexerFactory, sequenceLexerFactory);
            var unreservedLexerFactory = new UnreservedLexerFactory(alphaLexerFactory, digitLexerFactory, caseInsensitiveTerminalLexerFactory, alternativeLexerFactory);
            var pathCharacterLexerFactory = new PathCharacterLexerFactory(unreservedLexerFactory, percentEncodingLexerFactory, subcomponentsDelimiterLexerFactory, caseInsensitiveTerminalLexerFactory, alternativeLexerFactory);
            var repetitionLexerFactory = new RepetitionLexerFactory();
            var factory = new FragmentLexerFactory(alternativeLexerFactory, pathCharacterLexerFactory, repetitionLexerFactory, caseInsensitiveTerminalLexerFactory);
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
