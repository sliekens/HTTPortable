namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class AbsoluteUriLexerFactory : ILexerFactory<AbsoluteUri>
    {
        private readonly ILexerFactory<HierarchicalPart> hierarchicalPartLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexerFactory<Query> queryLexerFactory;

        private readonly ILexerFactory<Scheme> schemeLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public AbsoluteUriLexerFactory(
            ISequenceLexerFactory sequenceLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ILexerFactory<Scheme> schemeLexerFactory,
            ILexerFactory<HierarchicalPart> hierarchicalPartLexerFactory,
            ILexerFactory<Query> queryLexerFactory)
        {
            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException("optionLexerFactory");
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

            this.sequenceLexerFactory = sequenceLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.schemeLexerFactory = schemeLexerFactory;
            this.hierarchicalPartLexerFactory = hierarchicalPartLexerFactory;
            this.queryLexerFactory = queryLexerFactory;
        }

        public ILexer<AbsoluteUri> Create()
        {
            var scheme = this.schemeLexerFactory.Create();
            var colon = this.terminalLexerFactory.Create(@":", StringComparer.Ordinal);
            var hierPart = this.hierarchicalPartLexerFactory.Create();
            var qm = this.terminalLexerFactory.Create(@"?", StringComparer.Ordinal);
            var query = this.queryLexerFactory.Create();
            var queryPart = this.sequenceLexerFactory.Create(qm, query);
            var optQuery = this.optionLexerFactory.Create(queryPart);
            var innerLexer = this.sequenceLexerFactory.Create(scheme, colon, hierPart, optQuery);
            return new AbsoluteUriLexer(innerLexer);
        }
    }
}