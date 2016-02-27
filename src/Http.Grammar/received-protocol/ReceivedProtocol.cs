namespace Http.Grammar
{
    using TextFx.ABNF;

    public class ReceivedProtocol : Concatenation
    {
        public ReceivedProtocol(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
