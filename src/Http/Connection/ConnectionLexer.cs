using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.Connection
{
    public sealed class ConnectionLexer : Lexer<Connection>
    {
        public ConnectionLexer([NotNull] ILexer<RequiredDelimitedList> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<RequiredDelimitedList> InnerLexer { get; }

        protected override IEnumerable<Connection> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var requiredDelimitedList in InnerLexer.Read(scanner, context))
            {
                yield return new Connection(requiredDelimitedList);
            }
        }
    }
}
