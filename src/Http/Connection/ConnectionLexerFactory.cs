using System;
using Http.connection_option;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Connection
{
    public sealed class ConnectionLexerFactory : RuleLexerFactory<Connection>
    {
        static ConnectionLexerFactory()
        {
            Default = new ConnectionLexerFactory(
                Http.RequiredDelimitedListLexerFactory.Default,
                connection_option.ConnectionOptionLexerFactory.Default.Singleton());
        }

        public ConnectionLexerFactory(
            [NotNull] IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory,
            [NotNull] ILexerFactory<ConnectionOption> connectionOptionLexerFactory)
        {
            if (requiredDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requiredDelimitedListLexerFactory));
            }
            if (connectionOptionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionOptionLexerFactory));
            }
            RequiredDelimitedListLexerFactory = requiredDelimitedListLexerFactory;
            ConnectionOptionLexerFactory = connectionOptionLexerFactory;
        }

        [NotNull]
        public static ConnectionLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<ConnectionOption> ConnectionOptionLexerFactory { get; set; }

        [NotNull]
        public IRequiredDelimitedListLexerFactory RequiredDelimitedListLexerFactory { get; set; }

        public override ILexer<Connection> Create()
        {
            var innerLexer = RequiredDelimitedListLexerFactory.Create(ConnectionOptionLexerFactory.Create());
            return new ConnectionLexer(innerLexer);
        }
    }
}
