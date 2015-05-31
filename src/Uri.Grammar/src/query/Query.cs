namespace Uri.Grammar
{
    using SLANG;

    public class Query : Repetition
    {
        public Query(Repetition sequence)
            : base(sequence)
        {
        }
    }
}