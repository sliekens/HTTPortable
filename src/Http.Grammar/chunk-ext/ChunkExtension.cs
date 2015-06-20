namespace Http.Grammar
{
    using TextFx.ABNF;

    public class ChunkExtension : Repetition
    {
        public ChunkExtension(Repetition repetition)
            : base(repetition)
        {
        }
    }
}