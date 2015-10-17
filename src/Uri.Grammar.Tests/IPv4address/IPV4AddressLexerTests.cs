namespace Uri.Grammar
{
    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    using Xunit;

    public class IPV4AddressLexerTests
    {
        [Theory]
        [InlineData(@"0.0.0.0")]
        [InlineData(@"10.0.0.0")]
        [InlineData(@"127.0.0.0")]
        [InlineData(@"169.254.0.0")]
        [InlineData(@"172.16.0.0")]
        [InlineData(@"192.0.0.0")]
        [InlineData(@"192.0.2.0")]
        [InlineData(@"192.88.99.0")]
        [InlineData(@"192.168.0.0")]
        [InlineData(@"198.18.0.0")]
        [InlineData(@"198.51.100.0")]
        [InlineData(@"203.0.113.0")]
        [InlineData(@"224.0.0.0")]
        [InlineData(@"240.0.0.0")]
        [InlineData(@"255.255.255.255")]
        public void Read_ShouldSucceed(string input)
        {
            var terminalLexerFactory = new TerminalLexerFactory();
            var sequenceLexerFactory = new SequenceLexerFactory();
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var repetitionLexerFactory = new RepetitionLexerFactory();
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var decimalOctetLexerFactory = new DecimalOctetLexerFactory(
                valueRangeLexerFactory,
                terminalLexerFactory,
                alternativeLexerFactory,
                repetitionLexerFactory,
                digitLexerFactory,
                sequenceLexerFactory);
            var factory = new IPV4AddressLexerFactory(
                sequenceLexerFactory,
                terminalLexerFactory,
                decimalOctetLexerFactory);
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = lexer.Read(scanner, null);
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.NotNull(result.Element);
                Assert.Equal(input, result.Element.Text);
            }
        }
    }
}