using System;
using Http.token;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.protocol_name
{
    public sealed class ProtocolNameLexerFactory : RuleLexerFactory<ProtocolName>
    {
        static ProtocolNameLexerFactory()
        {
            Default = new ProtocolNameLexerFactory(token.TokenLexerFactory.Default.Singleton());
        }

        public ProtocolNameLexerFactory([NotNull] ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }
            TokenLexerFactory = tokenLexerFactory;
        }

        [NotNull]
        public static ProtocolNameLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Token> TokenLexerFactory { get; set; }

        public override ILexer<ProtocolName> Create()
        {
            var innerLexer = TokenLexerFactory.Create();
            return new ProtocolNameLexer(innerLexer);
        }
    }
}
