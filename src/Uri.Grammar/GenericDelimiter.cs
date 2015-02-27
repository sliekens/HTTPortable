using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class GenericDelimiter : Element
    {
        public GenericDelimiter(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data == ':'
                              || data == '/'
                              || data == '?'
                              || data == '#'
                              || data == '['
                              || data == ']'
                              || data == '@');
        }
    }
}