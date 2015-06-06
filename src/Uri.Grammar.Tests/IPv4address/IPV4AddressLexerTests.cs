namespace Uri.Grammar
{
    using SLANG;
    using SLANG.Core.DIGIT;

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
            var caseInsensitiveTerminalLexerFactory = new CaseInsensitiveTerminalLexerFactory();

            var sequenceLexerFactory = new SequenceLexerFactory();
            var stringLexerFactory = new StringLexerFactory(caseInsensitiveTerminalLexerFactory);
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var repetitionLexerFactory = new RepetitionLexerFactory();
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var decimalOctetLexerFactory = new DecimalOctetLexerFactory(
                valueRangeLexerFactory,
                stringLexerFactory,
                alternativeLexerFactory,
                repetitionLexerFactory,
                digitLexerFactory,
                sequenceLexerFactory);
            var factory = new IPV4AddressLexerFactory(
                sequenceLexerFactory,
                stringLexerFactory,
                decimalOctetLexerFactory);
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