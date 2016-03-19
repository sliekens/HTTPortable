namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class UniformResourceIdentifierLexerFactory : ILexerFactory<UniformResourceIdentifier>
    {
        private readonly ILexerFactory<Fragment> fragmentLexerFactory;

        private readonly ILexerFactory<HierarchicalPart> hierarchicalPartLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexerFactory<Query> queryLexerFactory;

        private readonly ILexerFactory<Scheme> schemeLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public UniformResourceIdentifierLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Scheme> schemeLexerFactory,
            ILexerFactory<HierarchicalPart> hierarchicalPartLexerFactory,
            ILexerFactory<Query> queryLexerFactory,
            ILexerFactory<Fragment> fragmentLexerFactory)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (schemeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(schemeLexerFactory));
            }

            if (hierarchicalPartLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hierarchicalPartLexerFactory));
            }

            if (queryLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(queryLexerFactory));
            }

            if (fragmentLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(fragmentLexerFactory));
            }

            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.schemeLexerFactory = schemeLexerFactory;
            this.hierarchicalPartLexerFactory = hierarchicalPartLexerFactory;
            this.queryLexerFactory = queryLexerFactory;
            this.fragmentLexerFactory = fragmentLexerFactory;
        }

        public ILexer<UniformResourceIdentifier> Create()
        {
            var scheme = schemeLexerFactory.Create();
            var colon = terminalLexerFactory.Create(@":", StringComparer.Ordinal);
            var hierPart = hierarchicalPartLexerFactory.Create();
            var qm = terminalLexerFactory.Create(@"?", StringComparer.Ordinal);
            var query = queryLexerFactory.Create();
            var queryPart = concatenationLexerFactory.Create(qm, query);
            var optQuery = optionLexerFactory.Create(queryPart);
            var ht = terminalLexerFactory.Create(@"#", StringComparer.Ordinal);
            var fragment = fragmentLexerFactory.Create();
            var fragmentPart = concatenationLexerFactory.Create(ht, fragment);
            var optFragment = optionLexerFactory.Create(fragmentPart);
            var innerLexer = concatenationLexerFactory.Create(scheme, colon, hierPart, optQuery, optFragment);
            return new UniformResourceIdentifierLexer(innerLexer);
        }
    }
}