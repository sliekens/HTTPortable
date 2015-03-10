namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class TransferCodingLexer : Lexer<TransferCoding>
    {
        private readonly ILexer<TransferExtension> transferExtensionLexer;

        public TransferCodingLexer(ILexer<TransferExtension> transferExtensionLexer)
            : base("transfer-coding")
        {
            Contract.Requires(transferExtensionLexer != null);
            this.transferExtensionLexer = transferExtensionLexer;
        }

        public override bool TryRead(ITextScanner scanner, out TransferCoding element)
        {
            if (scanner.EndOfInput)
            {
                element = default(TransferCoding);
                return false;
            }

            var context = scanner.GetContext();
            Element coding;
            if (TryReadTerminal(scanner, "chunked", out coding))
            {
                element = new TransferCoding(coding, context);
                return true;
            }

            if (TryReadTerminal(scanner, "compress", out coding))
            {
                element = new TransferCoding(coding, context);
                return true;
            }

            if (TryReadTerminal(scanner, "deflate", out coding))
            {
                element = new TransferCoding(coding, context);
                return true;
            }

            if (TryReadTerminal(scanner, "gzip", out coding))
            {
                element = new TransferCoding(coding, context);
                return true;
            }

            TransferExtension extension;
            if (this.transferExtensionLexer.TryRead(scanner, out extension))
            {
                element = new TransferCoding(extension, context);
                return true;
            }

            element = default(TransferCoding);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.transferExtensionLexer != null);
        }
    }
}