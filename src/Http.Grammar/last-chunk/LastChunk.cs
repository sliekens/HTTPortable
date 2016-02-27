namespace Http.Grammar
{
    using TextFx.ABNF;

    public class LastChunk : Concatenation
    {
        public LastChunk(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}