namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    public class BadWhiteSpace : Element
    {
        public BadWhiteSpace(OptionalWhiteSpace optionalWhiteSpace, ITextContext context)
            : base(optionalWhiteSpace.Data, context)
        {
            Contract.Requires(optionalWhiteSpace != null);
            Contract.Requires(context != null);
        }
    }
}