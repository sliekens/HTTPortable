using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.TE
{
    public sealed class TransferEncodingCollectionLexer : Lexer<TransferEncodingCollection>
    {
        public TransferEncodingCollectionLexer([NotNull] ILexer<OptionalDelimitedList> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<OptionalDelimitedList> InnerLexer { get; }

        protected override IEnumerable<TransferEncodingCollection> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var optionalDelimitedList in InnerLexer.Read(scanner, context))
            {
                yield return new TransferEncodingCollection(optionalDelimitedList);
            }
        }
    }
}
