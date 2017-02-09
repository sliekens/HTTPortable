using System;
using Http.protocol;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Upgrade
{
    public sealed class UpgradeLexerFactory : RuleLexerFactory<Upgrade>
    {
        static UpgradeLexerFactory()
        {
            Default = new UpgradeLexerFactory(
                Http.RequiredDelimitedListLexerFactory.Default,
                ProtocolLexerFactory.Default.Singleton());
        }

        public UpgradeLexerFactory(
            [NotNull] IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory,
            [NotNull] ILexerFactory<Protocol> protocoLexerFactory)
        {
            if (requiredDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requiredDelimitedListLexerFactory));
            }
            if (protocoLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(protocoLexerFactory));
            }
            RequiredDelimitedListLexerFactory = requiredDelimitedListLexerFactory;
            ProtocoLexerFactory = protocoLexerFactory;
        }

        [NotNull]
        public static UpgradeLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Protocol> ProtocoLexerFactory { get; set; }

        [NotNull]
        public IRequiredDelimitedListLexerFactory RequiredDelimitedListLexerFactory { get; set; }

        public override ILexer<Upgrade> Create()
        {
            var innerLexer = RequiredDelimitedListLexerFactory.Create(ProtocoLexerFactory.Create());
            return new UpgradeLexer(innerLexer);
        }
    }
}
