namespace Uri.Grammar
{
    using SLANG;
    using SLANG.Core.ALPHA;
    using SLANG.Core.DIGIT;

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
            var stringLexerFactory = new StringLexerFactory(caseInsensitiveTerminalLexerFactory);
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var alphaLexerFactory = new AlphaLexerFactory(valueRangeLexerFactory, alternativeLexerFactory);
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var factory = new UnreservedLexerFactory(alphaLexerFactory, digitLexerFactory, stringLexerFactory, alternativeLexerFactory);
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
