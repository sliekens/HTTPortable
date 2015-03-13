namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using ProtocolName = Token;
    using ProtocolVersion = Token;

    public class ProtocolLexer : Lexer<Protocol>
    {
        private readonly ILexer<Token> protocolNameLexer;
        private readonly ILexer<Token> protocolVersionLexer;

        public ProtocolLexer(ILexer<ProtocolName> protocolNameLexer, ILexer<ProtocolVersion> protocolVersionLexer)
            : base("protocol")
        {
            this.protocolNameLexer = protocolNameLexer;
            this.protocolVersionLexer = protocolVersionLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Protocol element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Protocol);
                return false;
            }

            var context = scanner.GetContext();
            ProtocolName protocolName;
            if (!this.protocolNameLexer.TryRead(scanner, out protocolName))
            {
                element = default(Protocol);
                return false;
            }

            Option<Sequence<Element, ProtocolVersion>> optionalProtocolVersion;
            if (!this.TryReadOptionalProtocolVersion(scanner, out optionalProtocolVersion))
            {
                scanner.PutBack(protocolName.Data);
                element = default(Protocol);
                return false;
            }

            element = new Protocol(protocolName, optionalProtocolVersion, context);
            return true;
        }

        private bool TryReadOptionalProtocolVersion(ITextScanner scanner, out Option<Sequence<Element, ProtocolVersion>> element)
        {
            var context = scanner.GetContext();
            Sequence<Element, ProtocolVersion> protocolVersionPart;
            if (this.TryReadProtocolVersionPart(scanner, out protocolVersionPart))
            {
                element = new Option<Sequence<Element, Token>>(protocolVersionPart, context);
            }
            else
            {
                element = new Option<Sequence<Element, Token>>(context);
            }

            return true;
        }

        private bool TryReadProtocolVersionPart(ITextScanner scanner, out Sequence<Element, ProtocolVersion> element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Sequence<Element, ProtocolVersion>);
                return false;
            }

            var context = scanner.GetContext();
            Element separator;
            if (!TryReadTerminal(scanner, @"/", out separator))
            {
                element = default(Sequence<Element, ProtocolVersion>);
                return false;
            }

            ProtocolVersion protocolVersion;
            if (!this.protocolVersionLexer.TryRead(scanner, out protocolVersion))
            {
                scanner.PutBack(separator.Data);
                element = default(Sequence<Element, ProtocolVersion>);
                return false;
            }

            element = new Sequence<Element, Token>(separator, protocolVersion, context);
            return true;
        }
    }
}