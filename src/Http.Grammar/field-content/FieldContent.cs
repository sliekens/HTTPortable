namespace Http.Grammar
{
    using TextFx.ABNF;

    public class FieldContent : Concatenation
    {
        public FieldContent(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}