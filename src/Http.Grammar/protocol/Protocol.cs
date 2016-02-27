namespace Http.Grammar
{
    using TextFx.ABNF;

    public class Protocol : Concatenation
    {
        public Protocol(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}