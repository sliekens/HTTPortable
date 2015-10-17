namespace Uri.Grammar
{
    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    using Xunit;

    public class UnreservedLexerTests
    {
        [Theory]
        [InlineData(@"a")]
        [InlineData(@"B")]
        [InlineData(@"c")]
        [InlineData(@"0")]
        [InlineData(@"1")]
        [InlineData(@"2")]
        [InlineData(@"-")]
        [InlineData(@".")]
        [InlineData(@"_")]
        [InlineData(@"~")]
        public void Read_ShouldSucceed(string input)
        {
            var caseInsensitiveTerminalLexerFactory = new CaseInsensitiveTerminalLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var alphaLexerFactory = new AlphaLexerFactory(valueRangeLexerFactory, alternativeLexerFactory);
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var factory = new UnreservedLexerFactory(alphaLexerFactory, digitLexerFactory, caseInsensitiveTerminalLexerFactory, alternativeLexerFactory);
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
