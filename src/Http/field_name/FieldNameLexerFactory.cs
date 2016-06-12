using System;
using Http.token;
using JetBrains.Annotations;
using Txt;
using Txt.Core;

namespace Http.field_name
{
    public class FieldNameLexerFactory : ILexerFactory<FieldName>
    {
        private readonly ILexer<Token> tokenLexer;

        public FieldNameLexerFactory([NotNull] ILexer<Token> tokenLexer)
        {
            if (tokenLexer == null)
            {
                throw new ArgumentNullException(nameof(tokenLexer));
            }
            this.tokenLexer = tokenLexer;
        }

        public ILexer<FieldName> Create()
        {
            return new FieldNameLexer(tokenLexer);
        }
    }
}
