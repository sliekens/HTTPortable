namespace Http.Grammar
{
    using TextFx.ABNF;

    public class ObsoleteFold : Concatenation
    {
        public ObsoleteFold(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}