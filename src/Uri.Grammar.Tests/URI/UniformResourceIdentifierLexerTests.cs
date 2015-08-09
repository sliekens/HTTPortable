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
            var stringLexerFactory = new StringLexerFactory(caseInsensitiveTerminalLexerFactory);
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
                stringLexerFactory);
            var unreservedLexerFactory = new UnreservedLexerFactory(
                alphaLexerFactory,
                digitLexerFactory,
                stringLexerFactory,
                alternativeLexerFactory);
            var hexadecimalDigitLexerFactory = new HexadecimalDigitLexerFactory(
                digitLexerFactory,
                stringLexerFactory,
                alternativeLexerFactory);
            var percentEncodingLexerFactory = new PercentEncodingLexerFactory(
                stringLexerFactory,
                hexadecimalDigitLexerFactory,
                sequenceLexerFactory);
            var subcomponentsDelimiterLexerFactory = new SubcomponentsDelimiterLexerFactory(
                stringLexerFactory,
                alternativeLexerFactory);
            var userInformationLexerFactory = new UserInformationLexerFactory(
                repetitionLexerFactory,
                alternativeLexerFactory,
                stringLexerFactory,
                unreservedLexerFactory,
                percentEncodingLexerFactory,
                subcomponentsDelimiterLexerFactory);
            var hexadecimalInt16LexerFactory = new HexadecimalInt16LexerFactory(
                repetitionLexerFactory,
                hexadecimalDigitLexerFactory);
            var decimalOctetLexerFactory = new DecimalOctetLexerFactory(
                valueRangeLexerFactory,
                stringLexerFactory,
                alternativeLexerFactory,
                repetitionLexerFactory,
                digitLexerFactory,
                sequenceLexerFactory);
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
            var ipv6AddressLexerFactory = new IPv6AddressLexerFactory(
                alternativeLexerFactory,
                sequenceLexerFactory,
                stringLexerFactory,
                repetitionLexerFactory,
                optionLexerFactory,
                hexadecimalInt16LexerFactory,
                leastSignificantInt32LexerFactory);
            var ipvFutureLexerFactory = new IPvFutureLexerFactory(
                stringLexerFactory,
                repetitionLexerFactory,
                sequenceLexerFactory,
                alternativeLexerFactory,
                hexadecimalDigitLexerFactory,
                unreservedLexerFactory,
                subcomponentsDelimiterLexerFactory);
            var ipLiteralLexerFactory = new IPLiteralLexerFactory(
                sequenceLexerFactory,
                alternativeLexerFactory,
                stringLexerFactory,
                ipv6AddressLexerFactory,
                ipvFutureLexerFactory);
            var encodingLexerFactory = new PercentEncodingLexerFactory(
                stringLexerFactory,
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
                stringLexerFactory,
                hostLexerFactory,
                portLexerFactory);
            var pathCharacterLexerFactory = new PathCharacterLexerFactory(
                unreservedLexerFactory,
                percentEncodingLexerFactory,
                subcomponentsDelimiterLexerFactory,
                stringLexerFactory,
                alternativeLexerFactory);
            var segmentLexerFactory = new SegmentLexerFactory(pathCharacterLexerFactory, repetitionLexerFactory);
            var segmentNonZeroLengthLexerFactory = new SegmentNonZeroLengthLexerFactory(
                pathCharacterLexerFactory,
                repetitionLexerFactory);
            var pathAbsoluteLexerFactory = new PathAbsoluteLexerFactory(
                stringLexerFactory,
                optionLexerFactory,
                sequenceLexerFactory,
                repetitionLexerFactory,
                segmentLexerFactory,
                segmentNonZeroLengthLexerFactory);
            var pathAbsoluteOrEmptyLexerFactory = new PathAbsoluteOrEmptyLexerFactory(
                repetitionLexerFactory,
                sequenceLexerFactory,
                stringLexerFactory,
                segmentLexerFactory);
            var pathEmptyLexerFactory = new PathEmptyLexerFactory(stringLexerFactory);
            var pathRootlessLexerFactory = new PathRootlessLexerFactory(
                sequenceLexerFactory,
                repetitionLexerFactory,
                stringLexerFactory,
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
                stringLexerFactory);
            var queryLexerFactory = new QueryLexerFactory(
                alternativeLexerFactory,
                pathCharacterLexerFactory,
                repetitionLexerFactory,
                stringLexerFactory);
            var fragmentLexerFactory = new FragmentLexerFactory(
                alternativeLexerFactory,
                pathCharacterLexerFactory,
                repetitionLexerFactory,
                stringLexerFactory);
            var factory = new UniformResourceIdentifierLexerFactory(
                sequenceLexerFactory,
                optionLexerFactory,
                stringLexerFactory,
                schemeLexerFactory,
                hierarchicalPartLexerFactory,
                queryLexerFactory,
                fragmentLexerFactory);
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