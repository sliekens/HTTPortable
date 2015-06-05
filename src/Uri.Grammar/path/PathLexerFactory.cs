namespace Uri.Grammar.path
{
    using System;

    using SLANG;

    public class PathLexerFactory : ILexerFactory<Path>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory;

        private readonly ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory;

        private readonly ILexerFactory<PathEmpty> pathEmptyLexerFactory;

        private readonly ILexerFactory<PathNoScheme> pathNoSchemeLexerFactory;

        private readonly ILexerFactory<PathRootless> pathRootlessLexerFactory;

        public PathLexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory,
            ILexerFactory<PathAbsolute> pathAbsoluteLexerFactory,
            ILexerFactory<PathNoScheme> pathNoSchemeLexerFactory,
            ILexerFactory<PathRootless> pathRootlessLexerFactory,
            ILexerFactory<PathEmpty> pathEmptyLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
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

            if (pathRootlessLexerFactory == null)
            {
                throw new ArgumentNullException("pathRootlessLexerFactory");
            }

            if (pathEmptyLexerFactory == null)
            {
                throw new ArgumentNullException("pathEmptyLexerFactory");
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.pathAbsoluteOrEmptyLexerFactory = pathAbsoluteOrEmptyLexerFactory;
            this.pathAbsoluteLexerFactory = pathAbsoluteLexerFactory;
            this.pathNoSchemeLexerFactory = pathNoSchemeLexerFactory;
            this.pathRootlessLexerFactory = pathRootlessLexerFactory;
            this.pathEmptyLexerFactory = pathEmptyLexerFactory;
        }

        public ILexer<Path> Create()
        {
            ILexer[] a =
                {
                    this.pathAbsoluteOrEmptyLexerFactory.Create(), this.pathAbsoluteLexerFactory.Create(),
                    this.pathNoSchemeLexerFactory.Create(), this.pathRootlessLexerFactory.Create(),
                    this.pathEmptyLexerFactory.Create()
                };

            var b = this.alternativeLexerFactory.Create(a);
            return new PathLexer(b);
        }
    }
}