namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class ChunkExtensionValue : Alternative<Token, QuotedString>
    {
        public ChunkExtensionValue(Element element, int alternative, ITextContext context)
            : base(element, alternative, context)
        {
        }
    }
}
