namespace Http.Grammar
{
    using TextFx.ABNF;

    public class HttpName : TerminalString
    {
        public HttpName(TerminalString element)
            : base(element)
        {
        }
    }
}
