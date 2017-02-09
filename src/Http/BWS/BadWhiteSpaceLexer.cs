using System;
using System.Collections.Generic;
using Http.OWS;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.BWS
{
    public class BadWhiteSpaceLexer : Lexer<BadWhiteSpace>
    {
        public BadWhiteSpaceLexer([NotNull] ILexer<OptionalWhiteSpace> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<OptionalWhiteSpace> InnerLexer { get; }

        protected override IEnumerable<BadWhiteSpace> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var optionalWhiteSpace in InnerLexer.Read(scanner, context))
            {
                yield return new BadWhiteSpace(optionalWhiteSpace);
            }
        }
    }
}
