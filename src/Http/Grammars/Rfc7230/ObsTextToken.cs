namespace Http.Grammars.Rfc7230
{
    using System.Diagnostics.Contracts;
    using Text.Scanning;

    public class ObsTextToken : Token
    {
        public ObsTextToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data >= '\u0080' && data <= '\u00FF');
        }
    }
}