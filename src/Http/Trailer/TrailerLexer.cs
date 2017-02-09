using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Trailer
{
    public sealed class TrailerLexer : Lexer<Trailer>
    {
        public TrailerLexer([NotNull] ILexer<RequiredDelimitedList> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<RequiredDelimitedList> InnerLexer { get; }

        protected override IEnumerable<Trailer> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var requiredDelimitedList in InnerLexer.Read(scanner, context))
            {
                yield return new Trailer(requiredDelimitedList);
            }
        }
    }
}
