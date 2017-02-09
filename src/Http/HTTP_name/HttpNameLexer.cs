using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.HTTP_name
{
    public sealed class HttpNameLexer : Lexer<HttpName>
    {
        public HttpNameLexer([NotNull] ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Terminal> InnerLexer { get; }

        protected override IEnumerable<HttpName> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new HttpName(terminal);
            }
        }
    }
}
