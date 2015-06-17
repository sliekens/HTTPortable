namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class Query : Repetition
    {
        public Query(Repetition sequence)
            : base(sequence)
        {
        }
    }
}