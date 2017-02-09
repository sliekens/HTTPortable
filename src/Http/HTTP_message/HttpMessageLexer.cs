using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.HTTP_message
{
    public sealed class HttpMessageLexer : Lexer<HttpMessage>
    {
        public HttpMessageLexer([NotNull] ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Concatenation> InnerLexer { get; }

        protected override IEnumerable<HttpMessage> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new HttpMessage(concatenation);
            }
        }
    }
}
