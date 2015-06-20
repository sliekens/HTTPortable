namespace Http.Grammar
{
    using TextFx.ABNF;

    public class RequestTarget : Alternative
    {
        public RequestTarget(Alternative alternative)
            : base(alternative)
        {
        }
    }
}