using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.obs_fold
{
    public sealed class ObsoleteFoldLexer : Lexer<ObsoleteFold>
    {
        public ObsoleteFoldLexer([NotNull] ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Concatenation> InnerLexer { get; }

        protected override IEnumerable<ObsoleteFold> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new ObsoleteFold(concatenation);
            }
        }
    }
}
