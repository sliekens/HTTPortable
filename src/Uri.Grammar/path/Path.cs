namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class Path : Alternative
    {
        public Path(Alternative alternative)
            : base(alternative)
        {
        }
    }
}