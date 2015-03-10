namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
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