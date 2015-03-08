using System.Diagnostics.Contracts;


namespace Uri.Grammar
{
    using SLANG;

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