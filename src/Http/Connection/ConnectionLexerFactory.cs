using System;
using Http.connection_option;
using JetBrains.Annotations;
using Txt;
using Txt.Core;

namespace Http.Connection
{
    public class ConnectionLexerFactory : ILexerFactory<Connection>
    {
        private readonly ILexer<ConnectionOption> connectionOptionLexer;

        private readonly IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory;

        public ConnectionLexerFactory(
            [NotNull] IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory,
            [NotNull] ILexer<ConnectionOption> connectionOptionLexer)
        {
            if (requiredDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requiredDelimitedListLexerFactory));
            }
            if (connectionOptionLexer == null)
            {
                throw new ArgumentNullException(nameof(connectionOptionLexer));
            }
            this.requiredDelimitedListLexerFactory = requiredDelimitedListLexerFactory;
            this.connectionOptionLexer = connectionOptionLexer;
        }

        public ILexer<Connection> Create()
        {
            return new ConnectionLexer(requiredDelimitedListLexerFactory.Create(connectionOptionLexer));
        }
    }
}
