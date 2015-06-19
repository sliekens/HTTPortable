namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class LeastSignificantInt32 : Alternative
    {
        public LeastSignificantInt32(Alternative alternative)
            : base(alternative)
        {
        }
    }
}