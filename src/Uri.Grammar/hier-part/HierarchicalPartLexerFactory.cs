namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class HierarchicalPartLexerFactory : ILexerFactory<HierarchicalPart>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<Authority> authorityLexerFactory;

        private readonly ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory;

        private readonly ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory;

        private readonly ILexerFactory<PathEmpty> pathEmptyLexerFactory;

        private readonly ILexerFactory<PathRootless> pathRootlessLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public HierarchicalPartLexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            ILexerFactory<Authority> authorityLexerFactory,
            ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory,
            ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory,
            ILexerFactory<PathEmpty> pathEmptyLexerFactory,
            ILexerFactory<PathRootless> pathRootlessLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            IStringLexerFactory stringLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (authorityLexerFactory == null)
            {
                throw new ArgumentNullException("authorityLexerFactory");
            }

            if (pathAbsoluteLexerFactory == null)
            {
                throw new ArgumentNullException("pathAbsoluteLexerFactory");
            }

            if (pathAbsoluteOrEmptyLexerFactory == null)
            {
                throw new ArgumentNullException("pathAbsoluteOrEmptyLexerFactory");
            }

            if (pathEmptyLexerFactory == null)
            {
                throw new ArgumentNullException("pathEmptyLexerFactory");
            }

            if (pathRootlessLexerFactory == null)
            {
                throw new ArgumentNullException("pathRootlessLexerFactory");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.authorityLexerFactory = authorityLexerFactory;
            this.pathAbsoluteLexerFactory = pathAbsoluteLexerFactory;
            this.pathAbsoluteOrEmptyLexerFactory = pathAbsoluteOrEmptyLexerFactory;
            this.pathEmptyLexerFactory = pathEmptyLexerFactory;
            this.pathRootlessLexerFactory = pathRootlessLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
        }

        public ILexer<HierarchicalPart> Create()
        {
            var delim = this.stringLexerFactory.Create(@"//");
            var authority = this.authorityLexerFactory.Create();
            var pathAbEmpty = this.pathAbsoluteOrEmptyLexerFactory.Create();
            var seq = this.sequenceLexerFactory.Create(delim, authority, pathAbEmpty);
            var pathAbsolute = this.pathAbsoluteLexerFactory.Create();
            var pathRootless = this.pathRootlessLexerFactory.Create();
            var pathEmpty = this.pathEmptyLexerFactory.Create();
            var innerLexer = this.alternativeLexerFactory.Create(seq, pathAbsolute, pathRootless, pathEmpty);
            return new HierarchicalPartLexer(innerLexer);

        }
    }
}