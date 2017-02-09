using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.reason_phrase
{
    public sealed class ReasonPhraseLexer : Lexer<ReasonPhrase>
    {
        public ReasonPhraseLexer([NotNull] ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Repetition> InnerLexer { get; }

        protected override IEnumerable<ReasonPhrase> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var repetition in InnerLexer.Read(scanner, context))
            {
                yield return new ReasonPhrase(repetition);
            }
        }
    }
}
