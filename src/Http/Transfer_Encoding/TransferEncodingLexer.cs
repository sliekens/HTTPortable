using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Transfer_Encoding
{
    public sealed class TransferEncodingLexer : Lexer<TransferEncoding>
    {
        public TransferEncodingLexer([NotNull] ILexer<RequiredDelimitedList> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<RequiredDelimitedList> InnerLexer { get; }

        protected override IEnumerable<TransferEncoding> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var requiredDelimitedList in InnerLexer.Read(scanner, context))
            {
                yield return new TransferEncoding(requiredDelimitedList);
            }
        }
    }
}
