namespace Http.Grammar
{
    using TextFx.ABNF;

    public class Host : Concatenation
    {
        public Host(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}