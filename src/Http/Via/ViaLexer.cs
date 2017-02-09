using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Via
{
    public sealed class ViaLexer : Lexer<Via>
    {
        public ViaLexer([NotNull] ILexer<RequiredDelimitedList> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<RequiredDelimitedList> InnerLexer { get; }

        protected override IEnumerable<Via> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var requiredDelimitedList in InnerLexer.Read(scanner, context))
            {
                yield return new Via(requiredDelimitedList);
            }
        }
    }
}
