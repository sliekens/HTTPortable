namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    public class TransferParameter : Element
    {
        private readonly Token name;

        private readonly Element value;

        public TransferParameter(Token name, Token value, ITextContext context)
            : base(string.Concat(name, "=", value), context)
        {
            Contract.Requires(name != null);
            Contract.Requires(value != null);
            Contract.Requires(context != null);
            this.name = name;
            this.value = value;
        }

        public TransferParameter(Token name, QuotedString value, ITextContext context)
            : base(string.Concat(name, "=", value), context)
        {
            Contract.Requires(name != null);
            Contract.Requires(value != null);
            Contract.Requires(context != null);
            this.name = name;
            this.value = value;
        }

        public Token Name
        {
            get
            {
                return this.name;
            }
        }

        public Element Value
        {
            get
            {
                return this.value;
            }
        }
    }
}