namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class IPLiteral : Element
    {
        public IPLiteral(Element openingBracket, IPv6Address address, Element closingBracket, ITextContext context)
            : base(string.Concat(openingBracket, address, closingBracket), context)
        {
            Contract.Requires(openingBracket != null);
            Contract.Requires(openingBracket.Data == "[");
            Contract.Requires(address != null);
            Contract.Requires(closingBracket != null);
            Contract.Requires(closingBracket.Data == "]");
        }

        public IPLiteral(Element openingBracket, IPvFuture address, Element closingBracket, ITextContext context)
            : base(string.Concat(openingBracket, address, closingBracket), context)
        {
            Contract.Requires(openingBracket != null);
            Contract.Requires(openingBracket.Data == "[");
            Contract.Requires(address != null);
            Contract.Requires(closingBracket != null);
            Contract.Requires(closingBracket.Data == "]");
        }
    }
}