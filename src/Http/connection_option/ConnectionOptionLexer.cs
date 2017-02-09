using System;
using System.Collections.Generic;
using Http.token;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.connection_option
{
    public sealed class ConnectionOptionLexer : Lexer<ConnectionOption>
    {
        public ConnectionOptionLexer([NotNull] ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Token> InnerLexer { get; }

        protected override IEnumerable<ConnectionOption> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var token in InnerLexer.Read(scanner, context))
            {
                yield return new ConnectionOption(token);
            }
        }
    }
}
