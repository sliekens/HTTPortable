namespace Http.Grammar.Rfc7230
{
    using SLANG;

    using ConnectionOption = Token;

    public class ConnectionLexer : ElementList3Lexer<ConnectionOption>
    {
        public ConnectionLexer()
            : base("Connection", new ConnectionOptionLexer())
        {
        }

        public ConnectionLexer(ILexer<ConnectionOption> elementLexer)
            : base("Connection", elementLexer)
        {
        }

        public ConnectionLexer(ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer, ILexer<ConnectionOption> elementLexer)
            : base("Connection", optionalWhiteSpaceLexer, elementLexer)
        {
        }
    }
}