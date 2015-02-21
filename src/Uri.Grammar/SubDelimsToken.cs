using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class SubDelimsToken : Element
    {
        public SubDelimsToken(char data, ITextContext context)
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