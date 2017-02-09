using System;
using System.Collections.Generic;
using Http.token;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.protocol_name
{
    public sealed class ProtocolNameLexer : Lexer<ProtocolName>
    {
        public ProtocolNameLexer([NotNull] ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Token> InnerLexer { get; }

        protected override IEnumerable<ProtocolName> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var token in InnerLexer.Read(scanner, context))
            {
                yield return new ProtocolName(token);
            }
        }
    }
}
