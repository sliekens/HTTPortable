using System;
using Http.token;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_name
{
    public sealed class FieldNameLexerFactory : RuleLexerFactory<FieldName>
    {
        static FieldNameLexerFactory()
        {
            Default = new FieldNameLexerFactory(token.TokenLexerFactory.Default.Singleton());
        }

        public FieldNameLexerFactory([NotNull] ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }
            TokenLexerFactory = tokenLexerFactory;
        }

        [NotNull]
        public static FieldNameLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Token> TokenLexerFactory { get; }

        public override ILexer<FieldName> Create()
        {
            var innerLexer = TokenLexerFactory.Create();
            return new FieldNameLexer(innerLexer);
        }
    }
}
