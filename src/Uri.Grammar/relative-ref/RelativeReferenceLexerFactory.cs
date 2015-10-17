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

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public RelativeReferenceLexerFactory(
            ISequenceLexerFactory sequenceLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<RelativePart> relativePartLexerFactory,
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

            if (relativePartLexerFactory == null)
            {
                throw new ArgumentNullException("relativePartLexerFactory");
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
            this.relativePartLexerFactory = relativePartLexerFactory;
            this.queryLexerFactory = queryLexerFactory;
            this.fragmentLexerFactory = fragmentLexerFactory;
        }

        public ILexer<RelativeReference> Create()
        {
            var relativePart = this.relativePartLexerFactory.Create();
            var qm = this.terminalLexerFactory.Create(@"?", StringComparer.Ordinal);
            var query = this.queryLexerFactory.Create();
            var queryPart = this.sequenceLexerFactory.Create(qm, query);
            var optQuery = this.optionLexerFactory.Create(queryPart);
            var ht = this.terminalLexerFactory.Create(@"#", StringComparer.Ordinal);
            var fragment = this.fragmentLexerFactory.Create();
            var fragmentPart = this.sequenceLexerFactory.Create(ht, fragment);
            var optFragment = this.optionLexerFactory.Create(fragmentPart);
            var innerLexer = this.sequenceLexerFactory.Create(relativePart, optQuery, optFragment);
            return new RelativeReferenceLexer(innerLexer);
        }
    }
}