namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class RelativeReferenceLexerFactory : ILexerFactory<RelativeReference>
    {
        private readonly ILexerFactory<Fragment> fragmentLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexerFactory<Query> queryLexerFactory;

        private readonly ILexerFactory<RelativePart> relativePartLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public RelativeReferenceLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<RelativePart> relativePartLexerFactory,
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

            if (relativePartLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(relativePartLexerFactory));
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
            this.relativePartLexerFactory = relativePartLexerFactory;
            this.queryLexerFactory = queryLexerFactory;
            this.fragmentLexerFactory = fragmentLexerFactory;
        }

        public ILexer<RelativeReference> Create()
        {
            var relativePart = relativePartLexerFactory.Create();
            var qm = terminalLexerFactory.Create(@"?", StringComparer.Ordinal);
            var query = queryLexerFactory.Create();
            var queryPart = concatenationLexerFactory.Create(qm, query);
            var optQuery = optionLexerFactory.Create(queryPart);
            var ht = terminalLexerFactory.Create(@"#", StringComparer.Ordinal);
            var fragment = fragmentLexerFactory.Create();
            var fragmentPart = concatenationLexerFactory.Create(ht, fragment);
            var optFragment = optionLexerFactory.Create(fragmentPart);
            var innerLexer = concatenationLexerFactory.Create(relativePart, optQuery, optFragment);
            return new RelativeReferenceLexer(innerLexer);
        }
    }
}