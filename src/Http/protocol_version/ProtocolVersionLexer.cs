using System;
using System.Collections.Generic;
using Http.token;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.protocol_version
{
    public sealed class ProtocolVersionLexer : Lexer<ProtocolVersion>
    {
        public ProtocolVersionLexer([NotNull] ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Token> InnerLexer { get; }

        protected override IEnumerable<ProtocolVersion> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var token in InnerLexer.Read(scanner, context))
            {
                yield return new ProtocolVersion(token);
            }
        }
    }
}
