namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class IPvFuture : Concatenation
    {
        public IPvFuture(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}