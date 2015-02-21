using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class ObsoletedText : Element
    {
        public ObsoletedText(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data >= '\u0080' && data <= '\u00FF');
        }
    }
}