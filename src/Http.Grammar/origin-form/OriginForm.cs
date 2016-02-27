namespace Http.Grammar
{
    using TextFx.ABNF;

    public class OriginForm : Concatenation
    {
        public OriginForm(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}