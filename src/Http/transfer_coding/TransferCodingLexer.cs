using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.transfer_coding
{
    public sealed class TransferCodingLexer : Lexer<TransferCoding>
    {
        public TransferCodingLexer([NotNull] ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Alternation> InnerLexer { get; }

        protected override IEnumerable<TransferCoding> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var alternation in InnerLexer.Read(scanner, context))
            {
                yield return new TransferCoding(alternation);
            }
        }
    }
}
