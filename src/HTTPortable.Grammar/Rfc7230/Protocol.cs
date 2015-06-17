namespace Http.Grammar.Rfc7230
{
    using SLANG;

    using ProtocolName = Token;
    using ProtocolVersion = Token;

    public class Protocol : Sequence<ProtocolName, Option<Sequence<Element, ProtocolVersion>>>
    {
        public Protocol(Token element1, Option<Sequence<Element, Token>> element2, ITextContext context)
            : base(element1, element2, context)
        {
        }
    }
}