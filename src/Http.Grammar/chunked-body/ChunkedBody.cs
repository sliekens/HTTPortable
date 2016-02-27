namespace Http.Grammar
{
    using TextFx.ABNF;

    public class ChunkedBody : Concatenation
    {
        public ChunkedBody(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}