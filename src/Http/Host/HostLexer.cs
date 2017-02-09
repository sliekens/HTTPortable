using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Host
{
    public sealed class HostLexer : Lexer<Host>
    {
        public HostLexer([NotNull] ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Concatenation> InnerLexer { get; }

        protected override IEnumerable<Host> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new Host(concatenation);
            }
        }
    }
}
