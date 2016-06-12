using System;
using Http.transfer_extension;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.transfer_coding
{
    public class TransferCodingLexerFactory : ILexerFactory<TransferCoding>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexer<TransferExtension> transferExtensionLexer;

        public TransferCodingLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<TransferExtension> transferExtensionLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (transferExtensionLexer == null)
            {
                throw new ArgumentNullException(nameof(transferExtensionLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.transferExtensionLexer = transferExtensionLexer;
        }

        public ILexer<TransferCoding> Create()
        {
            return
                new TransferCodingLexer(
                    alternationLexerFactory.Create(
                        terminalLexerFactory.Create(@"chunked", StringComparer.OrdinalIgnoreCase),
                        terminalLexerFactory.Create(@"compress", StringComparer.OrdinalIgnoreCase),
                        terminalLexerFactory.Create(@"deflate", StringComparer.OrdinalIgnoreCase),
                        terminalLexerFactory.Create(@"gzip", StringComparer.OrdinalIgnoreCase),
                        transferExtensionLexer));
        }
    }
}
