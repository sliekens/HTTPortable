namespace Http.Grammar.Rfc7230
{
    using System;
    using System.Diagnostics.Contracts;

    using SLANG;

    public class TransferCoding : Element
    {
        public TransferCoding(Element terminal, ITextContext context)
            : base(terminal.Data, context)
        {
            Contract.Requires(terminal != null);
            Contract.Requires(string.Equals(terminal.Data, "chunked", StringComparison.OrdinalIgnoreCase)
                || string.Equals(terminal.Data, "compress", StringComparison.OrdinalIgnoreCase)
                || string.Equals(terminal.Data, "deflate", StringComparison.OrdinalIgnoreCase)
                || string.Equals(terminal.Data, "gzip", StringComparison.OrdinalIgnoreCase));
            Contract.Requires(context != null);
        }

        public TransferCoding(TransferExtension extension, ITextContext context)
            : base(extension.Data, context)
        {
            Contract.Requires(extension != null);
            Contract.Requires(context != null);
        }
    }
}
