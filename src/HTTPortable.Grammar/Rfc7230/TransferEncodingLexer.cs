namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class TransferEncodingLexer : ElementList3Lexer<TransferEncoding, TransferCoding>
    {
        public TransferEncodingLexer()
            : this(new TransferCodingLexer())
        {
        }

        public TransferEncodingLexer(ILexer<TransferCoding> elementLexer)
            : base("Transfer-Encoding", elementLexer)
        {
        }

        public TransferEncodingLexer(ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer, ILexer<TransferCoding> elementLexer)
            : base("Transfer-Encoding", optionalWhiteSpaceLexer, elementLexer)
        {
        }

        public TransferEncodingLexer(ILexer<Repetition<Sequence<Element, OptionalWhiteSpace>>> element1Lexer, ILexer<TransferCoding> element2Lexer, ILexer<Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, TransferCoding>>>>> element3Lexer)
            : base("Transfer-Encoding", element1Lexer, element2Lexer, element3Lexer)
        {
        }

        protected override TransferEncoding CreateInstance(Repetition<Sequence<Element, OptionalWhiteSpace>> element1, TransferCoding element2, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, TransferCoding>>>> element3, ITextContext context)
        {
            return new TransferEncoding(element1, element2, element3, context);
        }
    }
}
