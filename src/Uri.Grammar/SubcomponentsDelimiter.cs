using System.Diagnostics.Contracts;


namespace Uri.Grammar
{
    using SLANG;

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