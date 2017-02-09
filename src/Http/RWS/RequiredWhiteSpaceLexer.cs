using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.RWS
{
    public sealed class RequiredWhiteSpaceLexer : Lexer<RequiredWhiteSpace>
    {
        public RequiredWhiteSpaceLexer([NotNull] ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Repetition> InnerLexer { get; }

        protected override IEnumerable<RequiredWhiteSpace> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var repetition in InnerLexer.Read(scanner, context))
            {
                yield return new RequiredWhiteSpace(repetition);
            }
        }
    }
}