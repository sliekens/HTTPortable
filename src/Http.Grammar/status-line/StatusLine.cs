namespace Http.Grammar
{
    using TextFx.ABNF;

    public class StatusLine : Concatenation
    {
        public StatusLine(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
