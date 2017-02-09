using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.transfer_parameter
{
    public sealed class TransferParameterLexer : Lexer<TransferParameter>
    {
        public TransferParameterLexer([NotNull] ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Concatenation> InnerLexer { get; }

        protected override IEnumerable<TransferParameter> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var concatenation in InnerLexer.Read(scanner, context))
            {
                yield return new TransferParameter(concatenation);
            }
        }
    }
}
