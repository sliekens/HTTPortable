namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class GenericDelimiter : Alternative
    {
        public GenericDelimiter(Alternative element)
            : base(element)
        {
        }
    }
}