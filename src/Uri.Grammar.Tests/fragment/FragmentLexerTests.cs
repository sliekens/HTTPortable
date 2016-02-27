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
            var terminalLexerFactory = new TerminalLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var subcomponentsDelimiterLexerFactory = new SubcomponentsDelimiterLexerFactory(terminalLexerFactory, alternativeLexerFactory);
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var concatenationLexerFactory = new ConcatenationLexerFactory();
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var hexadecimalDigitLexerFactory = new HexadecimalDigitLexerFactory(digitLexerFactory, terminalLexerFactory, alternativeLexerFactory);
            var alphaLexerFactory = new AlphaLexerFactory(valueRangeLexerFactory, alternativeLexerFactory);
            var percentEncodingLexerFactory = new PercentEncodingLexerFactory(terminalLexerFactory, hexadecimalDigitLexerFactory, concatenationLexerFactory);
            var unreservedLexerFactory = new UnreservedLexerFactory(alphaLexerFactory, digitLexerFactory, terminalLexerFactory, alternativeLexerFactory);
            var pathCharacterLexerFactory = new PathCharacterLexerFactory(unreservedLexerFactory, percentEncodingLexerFactory, subcomponentsDelimiterLexerFactory, terminalLexerFactory, alternativeLexerFactory);
            var repetitionLexerFactory = new RepetitionLexerFactory();
            var factory = new FragmentLexerFactory(alternativeLexerFactory, pathCharacterLexerFactory, repetitionLexerFactory, terminalLexerFactory);
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
