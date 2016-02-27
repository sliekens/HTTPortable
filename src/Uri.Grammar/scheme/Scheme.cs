namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class Scheme : Concatenation
    {
        public Scheme(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}