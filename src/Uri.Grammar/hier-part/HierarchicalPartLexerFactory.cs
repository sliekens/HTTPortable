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

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HierarchicalPartLexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            ILexerFactory<Authority> authorityLexerFactory,
            ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory,
            ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory,
            ILexerFactory<PathEmpty> pathEmptyLexerFactory,
            ILexerFactory<PathRootless> pathRootlessLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            if (authorityLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(authorityLexerFactory));
            }

            if (pathAbsoluteLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteLexerFactory));
            }

            if (pathAbsoluteOrEmptyLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteOrEmptyLexerFactory));
            }

            if (pathEmptyLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathEmptyLexerFactory));
            }

            if (pathRootlessLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathRootlessLexerFactory));
            }

            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.authorityLexerFactory = authorityLexerFactory;
            this.pathAbsoluteLexerFactory = pathAbsoluteLexerFactory;
            this.pathAbsoluteOrEmptyLexerFactory = pathAbsoluteOrEmptyLexerFactory;
            this.pathEmptyLexerFactory = pathEmptyLexerFactory;
            this.pathRootlessLexerFactory = pathRootlessLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<HierarchicalPart> Create()
        {
            var delim = this.terminalLexerFactory.Create(@"//", StringComparer.Ordinal);
            var authority = this.authorityLexerFactory.Create();
            var pathAbEmpty = this.pathAbsoluteOrEmptyLexerFactory.Create();
            var seq = this.concatenationLexerFactory.Create(delim, authority, pathAbEmpty);
            var pathAbsolute = this.pathAbsoluteLexerFactory.Create();
            var pathRootless = this.pathRootlessLexerFactory.Create();
            var pathEmpty = this.pathEmptyLexerFactory.Create();
            var innerLexer = this.alternativeLexerFactory.Create(seq, pathAbsolute, pathRootless, pathEmpty);
            return new HierarchicalPartLexer(innerLexer);

        }
    }
}