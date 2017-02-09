using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.trailer_part
{
    public sealed class TrailerPartLexer : Lexer<TrailerPart>
    {
        public TrailerPartLexer([NotNull] ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Repetition> InnerLexer { get; }

        protected override IEnumerable<TrailerPart> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var repetition in InnerLexer.Read(scanner, context))
            {
                yield return new TrailerPart(repetition);
            }
        }
    }
}
