namespace Http.Grammar
{
    using TextFx.ABNF;

    public class ChunkData : Repetition
    {
        public ChunkData(Repetition repetition)
            : base(repetition)
        {
        }
    }
}