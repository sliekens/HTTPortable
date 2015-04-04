namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using ProtocolName = Token;
    using ProtocolVersion = Token;

    public class ReceivedProtocol : Sequence<Option<Sequence<ProtocolName, Element>>, ProtocolVersion>
    {
        public ReceivedProtocol(Option<Sequence<Token, Element>> element1, Token element2, ITextContext context)
            : base(element1, element2, context)
        {
        }
    }
}
