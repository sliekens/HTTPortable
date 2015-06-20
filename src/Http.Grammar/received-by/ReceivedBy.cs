namespace Http.Grammar
{
    using TextFx.ABNF;

    public class ReceivedBy : Alternative
    {
        public ReceivedBy(Alternative alternative)
            : base(alternative)
        {
        }
    }
}
