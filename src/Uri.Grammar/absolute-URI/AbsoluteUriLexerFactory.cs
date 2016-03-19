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

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public AbsoluteUriLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ILexerFactory<Scheme> schemeLexerFactory,
            ILexerFactory<HierarchicalPart> hierarchicalPartLexerFactory,
            ILexerFactory<Query> queryLexerFactory)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
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

            this.concatenationLexerFactory = concatenationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.schemeLexerFactory = schemeLexerFactory;
            this.hierarchicalPartLexerFactory = hierarchicalPartLexerFactory;
            this.queryLexerFactory = queryLexerFactory;
        }

        public ILexer<AbsoluteUri> Create()
        {
            var scheme = schemeLexerFactory.Create();
            var colon = terminalLexerFactory.Create(@":", StringComparer.Ordinal);
            var hierPart = hierarchicalPartLexerFactory.Create();
            var qm = terminalLexerFactory.Create(@"?", StringComparer.Ordinal);
            var query = queryLexerFactory.Create();
            var queryPart = concatenationLexerFactory.Create(qm, query);
            var optQuery = optionLexerFactory.Create(queryPart);
            var innerLexer = concatenationLexerFactory.Create(scheme, colon, hierPart, optQuery);
            return new AbsoluteUriLexer(innerLexer);
        }
    }
}