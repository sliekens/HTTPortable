namespace Uri.Grammar
{
    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    using Xunit;

    public class IPV6AddressLexerTests
    {
        [Theory]

        // 6( h16 ":" ) ls32
        [InlineData(@"1:2:3:4:5:6:7:8")]

        // "::" 5( h16 ":" ) ls32
        [InlineData(@"::1:2:3:4:5:6:7")]

        // [ h16 ] "::" 4( h16 ":" ) ls32
        [InlineData(@"::1:2:3:4:5:6")]
        [InlineData(@"1::2:3:4:5:6:7")]

        // [ *1( h16 ":" ) h16 ] "::" 3( h16 ":" ) ls32
        [InlineData(@"::1:2:3:4:5")]
        [InlineData(@"1::2:3:4:5:6")]
        [InlineData(@"1:2::3:4:5:6:7")]

        // [ *2( h16 ":" ) h16 ] "::" 2( h16 ":" ) ls32
        [InlineData(@"::1:2:3:4")]
        [InlineData(@"1::2:3:4:5")]
        [InlineData(@"1:2::3:4:5:6")]
        [InlineData(@"1:2:3::4:5:6:7")]

        // [ *3( h16 ":" ) h16 ] "::"    h16 ":"   ls32
        [InlineData(@"::1:2:3")]
        [InlineData(@"1::2:3:4")]
        [InlineData(@"1:2::3:4:5")]
        [InlineData(@"1:2:3::4:5:6")]
        [InlineData(@"1:2:3:4::5:6:7")]

        // [ *4( h16 ":" ) h16 ] "::" ls32
        [InlineData(@"::1:2")]
        [InlineData(@"1::2:3")]
        [InlineData(@"1:2::3:4")]
        [InlineData(@"1:2:3::4:5")]
        [InlineData(@"1:2:3:4::5:6")]
        [InlineData(@"1:2:3:4:5::6:7")]

        // [ *5( h16 ":" ) h16 ] "::" h16
        [InlineData(@"::1")]
        [InlineData(@"1::2")]
        [InlineData(@"1:2::3")]
        [InlineData(@"1:2:3::4")]
        [InlineData(@"1:2:3:4::5")]
        [InlineData(@"1:2:3:4:5::6")]
        [InlineData(@"1:2:3:4:5:6::7")]

        //  [ *6( h16 ":" ) h16 ] "::"
        [InlineData(@"::")]
        [InlineData(@"1::")]
        [InlineData(@"1:2::")]
        [InlineData(@"1:2:3::")]
        [InlineData(@"1:2:3:4::")]
        [InlineData(@"1:2:3:4:5::")]
        [InlineData(@"1:2:3:4:5:6::")]
        [InlineData(@"1:2:3:4:5:6:7::")]
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
            var optionLexerFactory = new OptionLexerFactory();
            var hexadecimalDigitLexerFactory = new HexadecimalDigitLexerFactory(
                digitLexerFactory,
                stringLexerFactory,
                alternativeLexerFactory);
            var hexadecimalInt16LexerFactory = new HexadecimalInt16LexerFactory(
                repetitionLexerFactory,
                hexadecimalDigitLexerFactory);
            var ipv4AddressLexerFactory = new IPV4AddressLexerFactory(
                sequenceLexerFactory,
                stringLexerFactory,
                decimalOctetLexerFactory);
            var leastSignificantInt32LexerFactory = new LeastSignificantInt32LexerFactory(
                alternativeLexerFactory,
                sequenceLexerFactory,
                stringLexerFactory,
                hexadecimalInt16LexerFactory,
                ipv4AddressLexerFactory);
            var factory = new IPv6AddressLexerFactory(
                alternativeLexerFactory,
                sequenceLexerFactory,
                stringLexerFactory,
                repetitionLexerFactory,
                optionLexerFactory,
                hexadecimalInt16LexerFactory,
                leastSignificantInt32LexerFactory);
            var lexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                scanner.Read();
                var element = lexer.Read(scanner, null);
                Assert.NotNull(element);
                Assert.Equal(input, element.Text);
            }
        }
    }
}