namespace Http.Grammar
{
    using TextFx;
    using TextFx.ABNF;

    public class HttpName : Terminal
    {
        public HttpName(Terminal element)
            : base(element)
        {
        }
    }
}
