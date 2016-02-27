namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class IPLiteral : Concatenation
    {
        public IPLiteral(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}