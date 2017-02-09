using System;
using System.Collections.Generic;
using Http.token;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_name
{
    public sealed class FieldNameLexer : Lexer<FieldName>
    {
        public FieldNameLexer([NotNull] ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Token> InnerLexer { get; }

        protected override IEnumerable<FieldName> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var token in InnerLexer.Read(scanner, context))
            {
                yield return new FieldName(token);
            }
        }
    }
}
