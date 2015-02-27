using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class PathCharacter : Element
    {
        public PathCharacter(Unreserved unreserved, ITextContext context)
            : base(unreserved.Data, context)
        {
            Contract.Requires(unreserved != null);
        }

        public PathCharacter(PercentEncoding percentEncoding, ITextContext context)
            : base(percentEncoding.Data, context)
        {
            Contract.Requires(percentEncoding != null);
        }

        public PathCharacter(SubcomponentsDelimiter subcomponentsDelimiter, ITextContext context)
            : base(subcomponentsDelimiter.Data, context)
        {
            Contract.Requires(subcomponentsDelimiter != null);
        }

        public PathCharacter(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data == ':' || data == '@');
        }
    }
}