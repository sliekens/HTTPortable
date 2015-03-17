namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class TrailerLexer : ElementList3Lexer<Trailer, FieldName>
    {
        public TrailerLexer(string ruleName, ILexer<FieldName> elementLexer)
            : base(ruleName, elementLexer)
        {
        }

        public TrailerLexer(string ruleName, ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer, ILexer<FieldName> elementLexer)
            : base(ruleName, optionalWhiteSpaceLexer, elementLexer)
        {
        }

        public TrailerLexer(string ruleName, ILexer<Repetition<Sequence<Element, OptionalWhiteSpace>>> element1Lexer, ILexer<FieldName> element2Lexer, ILexer<Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, FieldName>>>>> element3Lexer)
            : base(ruleName, element1Lexer, element2Lexer, element3Lexer)
        {
        }

        protected override Trailer CreateInstance(Repetition<Sequence<Element, OptionalWhiteSpace>> element1, FieldName element2, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, FieldName>>>> element3, ITextContext context)
        {
            return new Trailer(element1, element2, element3, context);
        }
    }
}
