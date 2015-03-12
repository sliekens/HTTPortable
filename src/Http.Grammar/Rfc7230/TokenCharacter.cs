namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class TokenCharacter : Alternative<Element, Alpha, Digit>
    {
        public TokenCharacter(Element element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(element.Data == "!" || element.Data == "#" || element.Data == "$" || element.Data == "%" || element.Data == "&" || element.Data == "'" ||
                              element.Data == "*" || element.Data == "+" || element.Data == "-" || element.Data == "." || element.Data == "^" || element.Data == "_" ||
                              element.Data == "`" || element.Data == "|" || element.Data == "~");
            Contract.Requires(context != null);
        }

        public TokenCharacter(Alpha element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }

        public TokenCharacter(Digit element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }
    }
}