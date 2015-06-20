namespace Http.Grammar
{
    using TextFx.ABNF;

    public class Comment : Sequence
    {
        public Comment(Sequence sequence)
            : base(sequence)
        {
        }
    }
}