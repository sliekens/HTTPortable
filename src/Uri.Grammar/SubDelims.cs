using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class SubDelims : Element
    {
        public SubDelims(char data, ITextContext context)
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