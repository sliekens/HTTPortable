using System;
using Http.BWS;
using Http.quoted_string;
using Http.token;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.transfer_parameter
{
    public sealed class TransferParameterLexerFactory : RuleLexerFactory<TransferParameter>
    {
        static TransferParameterLexerFactory()
        {
            Default = new TransferParameterLexerFactory(
                token.TokenLexerFactory.Default.Singleton(),
                BWS.BadWhiteSpaceLexerFactory.Default.Singleton(),
                quoted_string.QuotedStringLexerFactory.Default.Singleton());
        }

        public TransferParameterLexerFactory(
            [NotNull] ILexerFactory<Token> tokenLexerFactory,
            [NotNull] ILexerFactory<BadWhiteSpace> badWhiteSpaceLexerFactory,
            [NotNull] ILexerFactory<QuotedString> quotedStringLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }
            if (badWhiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(badWhiteSpaceLexerFactory));
            }
            if (quotedStringLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(quotedStringLexerFactory));
            }
            TokenLexerFactory = tokenLexerFactory;
            BadWhiteSpaceLexerFactory = badWhiteSpaceLexerFactory;
            QuotedStringLexerFactory = quotedStringLexerFactory;
        }

        [NotNull]
        public static TransferParameterLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<BadWhiteSpace> BadWhiteSpaceLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<QuotedString> QuotedStringLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<Token> TokenLexerFactory { get; set; }

        public override ILexer<TransferParameter> Create()
        {
            var bws = BadWhiteSpaceLexerFactory.Create();
            var token = TokenLexerFactory.Create();
            var innerLexer = Concatenation.Create(
                token,
                bws,
                Terminal.Create(@"=", StringComparer.Ordinal),
                bws,
                Alternation.Create(token, QuotedStringLexerFactory.Create()));
            return new TransferParameterLexer(innerLexer);
        }
    }
}
