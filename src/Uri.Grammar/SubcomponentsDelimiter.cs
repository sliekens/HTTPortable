using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class SubcomponentsDelimiter : Element
    {
        public SubcomponentsDelimiter(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data == '!'
                              || data == '$'
                              || data == '&'
                              || data == '\''
                              || data == '('
                              || data == ')'
                              || data == '*'
                              || data == '+'
                              || data == ','
                              || data == ';'
                              || data == '=');
        }
    }
}