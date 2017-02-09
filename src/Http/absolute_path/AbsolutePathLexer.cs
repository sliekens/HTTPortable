using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.absolute_path
{
    public class AbsolutePathLexer : Lexer<AbsolutePath>
    {
        public AbsolutePathLexer([NotNull] ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Repetition> InnerLexer { get; }

        protected override IEnumerable<AbsolutePath> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var repetition in InnerLexer.Read(scanner, context))
            {
                yield return new AbsolutePath(repetition);
            }
        }
    }
}