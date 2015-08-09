namespace Uri.Grammar
{
    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    using Xunit;

    public class PathCharacterLexerTests
    {
        [Theory]
        [InlineData(@":")]
        [InlineData(@"@")]
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
            var factory = new PathCharacterLexerFactory(unreservedLexerFactory, percentEncodingLexerFactory, subcomponentsDelimiterLexerFactory, stringLexerFactory, alternativeLexerFactory);
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new PushbackInputStream(input.ToMemoryStream())))
            {
                scanner.Read();
                var element = lexer.Read(scanner, null);
                Assert.NotNull(element);
                Assert.Equal(input, element.Text);
            }
        }
    }
}
