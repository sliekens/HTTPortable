using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http
{
    public sealed class RequiredDelimitedListLexer : Lexer<RequiredDelimitedList>
    {
        public RequiredDelimitedListLexer([NotNull] ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Concatenation> InnerLexer { get; }

        protected override IEnumerable<RequiredDelimitedList> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new RequiredDelimitedList(concatenation);
            }
        }
    }
}
