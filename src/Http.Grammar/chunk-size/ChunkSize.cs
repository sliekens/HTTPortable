namespace Http.Grammar
{
    using TextFx.ABNF;

    public class ChunkSize : Repetition
    {
        public ChunkSize(Repetition repetition)
            : base(repetition)
        {
        }
    }
}