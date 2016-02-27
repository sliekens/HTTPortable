namespace Http.Grammar
{
    using TextFx.ABNF;

    public class PartialUri : Concatenation
    {
        public PartialUri(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}