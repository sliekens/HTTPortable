namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class Reserved : Alternative
    {
        public Reserved(Alternative alternative)
            : base(alternative)
        {
        }
    }
}