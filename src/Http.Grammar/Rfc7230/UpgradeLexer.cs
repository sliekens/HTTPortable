namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class UpgradeLexer : ElementList3Lexer<Protocol>
    {
        public UpgradeLexer()
            : this(new OptionalWhiteSpaceLexer(), new ProtocolLexer())
        {
        }

        public UpgradeLexer(ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer, ILexer<Protocol> elementLexer)
            : base("Upgrade", optionalWhiteSpaceLexer, elementLexer)
        {
        }
    }
}