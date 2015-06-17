namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class Unreserved : Alternative
    {
        public Unreserved(Alternative element)
            : base(element)
        {
        }
    }
}