namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class UpgradeLexer : ElementList3Lexer<Upgrade, Protocol>
    {
        public UpgradeLexer()
            : this(new OptionalWhiteSpaceLexer(), new ProtocolLexer())
        {
        }

        public UpgradeLexer(ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer, ILexer<Protocol> elementLexer)
            : base("Upgrade", optionalWhiteSpaceLexer, elementLexer)
        {
        }

        protected override Upgrade CreateInstance(Repetition<Sequence<Element, OptionalWhiteSpace>> element1, Protocol element2, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Protocol>>>> element3, ITextContext context)
        {
            return new Upgrade(element1, element2, element3, context);
        }
    }
}