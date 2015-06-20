namespace Http.Grammar
{
    using TextFx.ABNF;

    public class ChunkExtensionValue : Alternative
    {
        public ChunkExtensionValue(Alternative alternative)
            : base(alternative)
        {
        }
    }
}