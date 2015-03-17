namespace Http.Grammar.Rfc7230
{
    using SLANG;

    using ConnectionOption = Token;

    public class ConnectionLexer : ElementList3Lexer<Connection, ConnectionOption>
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

        protected override Connection CreateInstance(Repetition<Sequence<Element, OptionalWhiteSpace>> element1, Token element2, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Token>>>> element3, ITextContext context)
        {
            return new Connection(element1, element2, element3, context);
        }
    }
}