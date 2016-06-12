using System;
using Http.protocol;
using JetBrains.Annotations;
using Txt;
using Txt.Core;

namespace Http.Upgrade
{
    public class UpgradeLexerFactory : ILexerFactory<Upgrade>
    {
        private readonly ILexer<Protocol> protocolLexer;

        private readonly IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory;

        public UpgradeLexerFactory(
            [NotNull] IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory,
            [NotNull] ILexer<Protocol> protocolLexer)
        {
            if (requiredDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requiredDelimitedListLexerFactory));
            }
            if (protocolLexer == null)
            {
                throw new ArgumentNullException(nameof(protocolLexer));
            }
            this.requiredDelimitedListLexerFactory = requiredDelimitedListLexerFactory;
            this.protocolLexer = protocolLexer;
        }

        public ILexer<Upgrade> Create()
        {
            return new UpgradeLexer(requiredDelimitedListLexerFactory.Create(protocolLexer));
        }
    }
}
