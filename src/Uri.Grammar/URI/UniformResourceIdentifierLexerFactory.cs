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

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public UniformResourceIdentifierLexerFactory(
            ISequenceLexerFactory sequenceLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Scheme> schemeLexerFactory,
            ILexerFactory<HierarchicalPart> hierarchicalPartLexerFactory,
            ILexerFactory<Query> queryLexerFactory,
            ILexerFactory<Fragment> fragmentLexerFactory)
        {
            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException("optionLexerFactory");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            if (schemeLexerFactory == null)
            {
                throw new ArgumentNullException("schemeLexerFactory");
            }

            if (hierarchicalPartLexerFactory == null)
            {
                throw new ArgumentNullException("hierarchicalPartLexerFactory");
            }

            if (queryLexerFactory == null)
            {
                throw new ArgumentNullException("queryLexerFactory");
            }

            if (fragmentLexerFactory == null)
            {
                throw new ArgumentNullException("fragmentLexerFactory");
            }

            this.sequenceLexerFactory = sequenceLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.schemeLexerFactory = schemeLexerFactory;
            this.hierarchicalPartLexerFactory = hierarchicalPartLexerFactory;
            this.queryLexerFactory = queryLexerFactory;
            this.fragmentLexerFactory = fragmentLexerFactory;
        }

        public ILexer<UniformResourceIdentifier> Create()
        {
            var scheme = this.schemeLexerFactory.Create();
            var colon = this.terminalLexerFactory.Create(@":");
            var hierPart = this.hierarchicalPartLexerFactory.Create();
            var qm = this.terminalLexerFactory.Create(@"?");
            var query = this.queryLexerFactory.Create();
            var queryPart = this.sequenceLexerFactory.Create(qm, query);
            var optQuery = this.optionLexerFactory.Create(queryPart);
            var ht = this.terminalLexerFactory.Create(@"#");
            var fragment = this.fragmentLexerFactory.Create();
            var fragmentPart = this.sequenceLexerFactory.Create(ht, fragment);
            var optFragment = this.optionLexerFactory.Create(fragmentPart);
            var innerLexer = this.sequenceLexerFactory.Create(scheme, colon, hierPart, optQuery, optFragment);
            return new UniformResourceIdentifierLexer(innerLexer);
        }
    }
}