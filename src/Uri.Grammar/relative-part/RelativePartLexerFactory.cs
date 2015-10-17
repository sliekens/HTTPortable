namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class RelativePartLexerFactory : ILexerFactory<RelativePart>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<Authority> authorityLexerFactory;

        private readonly ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory;

        private readonly ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory;

        private readonly ILexerFactory<PathEmpty> pathEmptyLexerFactory;

        private readonly ILexerFactory<PathNoScheme> pathNoSchemeLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public RelativePartLexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Authority> authorityLexerFactory,
            ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory,
            ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory,
            ILexerFactory<PathNoScheme> pathNoSchemeLexerFactory,
            ILexerFactory<PathEmpty> pathEmptyLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            if (authorityLexerFactory == null)
            {
                throw new ArgumentNullException("authorityLexerFactory");
            }

            if (pathAbsoluteOrEmptyLexerFactory == null)
            {
                throw new ArgumentNullException("pathAbsoluteOrEmptyLexerFactory");
            }

            if (pathAbsoluteLexerFactory == null)
            {
                throw new ArgumentNullException("pathAbsoluteLexerFactory");
            }

            if (pathNoSchemeLexerFactory == null)
            {
                throw new ArgumentNullException("pathNoSchemeLexerFactory");
            }

            if (pathEmptyLexerFactory == null)
            {
                throw new ArgumentNullException("pathEmptyLexerFactory");
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.authorityLexerFactory = authorityLexerFactory;
            this.pathAbsoluteOrEmptyLexerFactory = pathAbsoluteOrEmptyLexerFactory;
            this.pathAbsoluteLexerFactory = pathAbsoluteLexerFactory;
            this.pathNoSchemeLexerFactory = pathNoSchemeLexerFactory;
            this.pathEmptyLexerFactory = pathEmptyLexerFactory;
        }

        public ILexer<RelativePart> Create()
        {
            var delim = this.terminalLexerFactory.Create(@"//", StringComparer.Ordinal);
            var authority = this.authorityLexerFactory.Create();
            var pathAbEmpty = this.pathAbsoluteOrEmptyLexerFactory.Create();
            var seq = this.sequenceLexerFactory.Create(delim, authority, pathAbEmpty);
            var pathAbsolute = this.pathAbsoluteLexerFactory.Create();
            var pathNoScheme = this.pathNoSchemeLexerFactory.Create();
            var pathEmpty = this.pathEmptyLexerFactory.Create();
            var innerLexer = this.alternativeLexerFactory.Create(seq, pathAbsolute, pathNoScheme, pathEmpty);
            return new RelativePartLexer(innerLexer);
        }
    }
}