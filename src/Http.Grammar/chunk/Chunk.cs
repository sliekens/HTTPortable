namespace Http.Grammar
{
    using TextFx.ABNF;

    public class Chunk : Concatenation
    {
        public Chunk(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}