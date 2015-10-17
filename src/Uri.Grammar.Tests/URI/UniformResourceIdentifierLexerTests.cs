namespace Uri.Grammar
{
    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    using Xunit;

    public class UniformResourceIdentifierLexerTests
    {
        [Theory]
        [InlineData(@"ftp://ftp.is.co.za/rfc/rfc1808.txt")]
        [InlineData(@"http://www.ietf.org/rfc/rfc2396.txt")]
        [InlineData(@"ldap://[2001:db8::7]/c=GB?objectClass?one")]
        [InlineData(@"mailto:John.Doe@example.com")]
        [InlineData(@"news:comp.infosystems.www.servers.unix")]
        [InlineData(@"tel:+1-816-555-1212")]
        [InlineData(@"telnet://192.0.2.16:80/")]
        [InlineData(@"urn:oasis:names:specification:docbook:dtd:xml:4.1.2")]
        public void Read_ShouldSucceed(string input)
        {
            var sequenceLexerFactory = new SequenceLexerFactory();
            var optionLexerFactory = new OptionLexerFactory();
            var caseInsensitiveTerminalLexerFactory = new CaseInsensitiveTerminalLexerFactory();
            var alternativeLexerFactory = new AlternativeLexerFactory();
            var repetitionLexerFactory = new RepetitionLexerFactory();
            var valueRangeLexerFactory = new ValueRangeLexerFactory();
            var alphaLexerFactory = new AlphaLexerFactory(valueRangeLexerFactory, alternativeLexerFactory);
            var digitLexerFactory = new DigitLexerFactory(valueRangeLexerFactory);
            var schemeLexerFactory = new SchemeLexerFactory(
                sequenceLexerFactory,
                alternativeLexerFactory,
                repetitionLexerFactory,
                alphaLexerFactory,
                digitLexerFactory,
                caseInsensitiveTerminalLexerFactory);
            var unreservedLexerFactory = new UnreservedLexerFactory(
                alphaLexerFactory,
                digitLexerFactory,
                caseInsensitiveTerminalLexerFactory,
                alternativeLexerFactory);
            var hexadecimalDigitLexerFactory = new HexadecimalDigitLexerFactory(
                digitLexerFactory,
                caseInsensitiveTerminalLexerFactory,
                alternativeLexerFactory);
            var percentEncodingLexerFactory = new PercentEncodingLexerFactory(
                caseInsensitiveTerminalLexerFactory,
                hexadecimalDigitLexerFactory,
                sequenceLexerFactory);
            var subcomponentsDelimiterLexerFactory = new SubcomponentsDelimiterLexerFactory(
                caseInsensitiveTerminalLexerFactory,
                alternativeLexerFactory);
            var userInformationLexerFactory = new UserInformationLexerFactory(
                repetitionLexerFactory,
                alternativeLexerFactory,
                caseInsensitiveTerminalLexerFactory,
                unreservedLexerFactory,
                percentEncodingLexerFactory,
                subcomponentsDelimiterLexerFactory);
            var hexadecimalInt16LexerFactory = new HexadecimalInt16LexerFactory(
                repetitionLexerFactory,
                hexadecimalDigitLexerFactory);
            var decimalOctetLexerFactory = new DecimalOctetLexerFactory(
                valueRangeLexerFactory,
                caseInsensitiveTerminalLexerFactory,
                alternativeLexerFactory,
                repetitionLexerFactory,
                digitLexerFactory,
                sequenceLexerFactory);
            var ipv4AddressLexerFactory = new IPV4AddressLexerFactory(
                sequenceLexerFactory,
                caseInsensitiveTerminalLexerFactory,
                decimalOctetLexerFactory);
            var leastSignificantInt32LexerFactory = new LeastSignificantInt32LexerFactory(
                alternativeLexerFactory,
                sequenceLexerFactory,
                caseInsensitiveTerminalLexerFactory,
                hexadecimalInt16LexerFactory,
                ipv4AddressLexerFactory);
            var ipv6AddressLexerFactory = new IPv6AddressLexerFactory(
                alternativeLexerFactory,
                sequenceLexerFactory,
                caseInsensitiveTerminalLexerFactory,
                repetitionLexerFactory,
                optionLexerFactory,
                hexadecimalInt16LexerFactory,
                leastSignificantInt32LexerFactory);
            var ipvFutureLexerFactory = new IPvFutureLexerFactory(
                caseInsensitiveTerminalLexerFactory,
                repetitionLexerFactory,
                sequenceLexerFactory,
                alternativeLexerFactory,
                hexadecimalDigitLexerFactory,
                unreservedLexerFactory,
                subcomponentsDelimiterLexerFactory);
            var ipLiteralLexerFactory = new IPLiteralLexerFactory(
                sequenceLexerFactory,
                alternativeLexerFactory,
                caseInsensitiveTerminalLexerFactory,
                ipv6AddressLexerFactory,
                ipvFutureLexerFactory);
            var encodingLexerFactory = new PercentEncodingLexerFactory(
                caseInsensitiveTerminalLexerFactory,
                hexadecimalDigitLexerFactory,
                sequenceLexerFactory);
            var registeredNameLexerFactory = new RegisteredNameLexerFactory(
                repetitionLexerFactory,
                alternativeLexerFactory,
                unreservedLexerFactory,
                encodingLexerFactory,
                subcomponentsDelimiterLexerFactory);
            var hostLexerFactory = new HostLexerFactory(
                alternativeLexerFactory,
                ipLiteralLexerFactory,
                ipv4AddressLexerFactory,
                registeredNameLexerFactory);
            var portLexerFactory = new PortLexerFactory(repetitionLexerFactory, digitLexerFactory);
            var authorityLexerFactory = new AuthorityLexerFactory(
                optionLexerFactory,
                sequenceLexerFactory,
                userInformationLexerFactory,
                caseInsensitiveTerminalLexerFactory,
                hostLexerFactory,
                portLexerFactory);
            var pathCharacterLexerFactory = new PathCharacterLexerFactory(
                unreservedLexerFactory,
                percentEncodingLexerFactory,
                subcomponentsDelimiterLexerFactory,
                caseInsensitiveTerminalLexerFactory,
                alternativeLexerFactory);
            var segmentLexerFactory = new SegmentLexerFactory(pathCharacterLexerFactory, repetitionLexerFactory);
            var segmentNonZeroLengthLexerFactory = new SegmentNonZeroLengthLexerFactory(
                pathCharacterLexerFactory,
                repetitionLexerFactory);
            var pathAbsoluteLexerFactory = new PathAbsoluteLexerFactory(
                caseInsensitiveTerminalLexerFactory,
                optionLexerFactory,
                sequenceLexerFactory,
                repetitionLexerFactory,
                segmentLexerFactory,
                segmentNonZeroLengthLexerFactory);
            var pathAbsoluteOrEmptyLexerFactory = new PathAbsoluteOrEmptyLexerFactory(
                repetitionLexerFactory,
                sequenceLexerFactory,
                caseInsensitiveTerminalLexerFactory,
                segmentLexerFactory);
            var pathEmptyLexerFactory = new PathEmptyLexerFactory(caseInsensitiveTerminalLexerFactory);
            var pathRootlessLexerFactory = new PathRootlessLexerFactory(
                sequenceLexerFactory,
                repetitionLexerFactory,
                caseInsensitiveTerminalLexerFactory,
                segmentLexerFactory,
                segmentNonZeroLengthLexerFactory);
            var hierarchicalPartLexerFactory = new HierarchicalPartLexerFactory(
                alternativeLexerFactory,
                authorityLexerFactory,
                pathAbsoluteLexerFactory,
                pathAbsoluteOrEmptyLexerFactory,
                pathEmptyLexerFactory,
                pathRootlessLexerFactory,
                sequenceLexerFactory,
                caseInsensitiveTerminalLexerFactory);
            var queryLexerFactory = new QueryLexerFactory(
                alternativeLexerFactory,
                pathCharacterLexerFactory,
                repetitionLexerFactory,
                caseInsensitiveTerminalLexerFactory);
            var fragmentLexerFactory = new FragmentLexerFactory(
                alternativeLexerFactory,
                pathCharacterLexerFactory,
                repetitionLexerFactory,
                caseInsensitiveTerminalLexerFactory);
            var factory = new UniformResourceIdentifierLexerFactory(
                sequenceLexerFactory,
                optionLexerFactory,
                caseInsensitiveTerminalLexerFactory,
                schemeLexerFactory,
                hierarchicalPartLexerFactory,
                queryLexerFactory,
                fragmentLexerFactory);
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