using System;
using Http.BWS;
using Http.quoted_string;
using Http.token;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;

namespace Http.transfer_parameter
{
    public class TransferParameterLexerFactory : ILexerFactory<TransferParameter>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<BadWhiteSpace> badWhiteSpaceLexer;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<QuotedString> quotedStringLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexer<Token> tokenLexer;

        public TransferParameterLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexer<Token> tokenLexer,
            [NotNull] ILexer<BadWhiteSpace> badWhiteSpaceLexer,
            [NotNull] ILexer<QuotedString> quotedStringLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (tokenLexer == null)
            {
                throw new ArgumentNullException(nameof(tokenLexer));
            }
            if (badWhiteSpaceLexer == null)
            {
                throw new ArgumentNullException(nameof(badWhiteSpaceLexer));
            }
            if (quotedStringLexer == null)
            {
                throw new ArgumentNullException(nameof(quotedStringLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.tokenLexer = tokenLexer;
            this.badWhiteSpaceLexer = badWhiteSpaceLexer;
            this.quotedStringLexer = quotedStringLexer;
        }

        public ILexer<TransferParameter> Create()
        {
            return
                new TransferParameterLexer(
                    concatenationLexerFactory.Create(
                        tokenLexer,
                        badWhiteSpaceLexer,
                        terminalLexerFactory.Create(@"=", StringComparer.Ordinal),
                        badWhiteSpaceLexer,
                        alternationLexerFactory.Create(tokenLexer, quotedStringLexer)));
        }
    }
}
