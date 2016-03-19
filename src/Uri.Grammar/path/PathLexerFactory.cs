namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

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
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            if (pathAbsoluteOrEmptyLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteOrEmptyLexerFactory));
            }

            if (pathAbsoluteLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteLexerFactory));
            }

            if (pathNoSchemeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathNoSchemeLexerFactory));
            }

            if (pathRootlessLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathRootlessLexerFactory));
            }

            if (pathEmptyLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathEmptyLexerFactory));
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
                    pathAbsoluteOrEmptyLexerFactory.Create(), pathAbsoluteLexerFactory.Create(),
                    pathNoSchemeLexerFactory.Create(), pathRootlessLexerFactory.Create(),
                    pathEmptyLexerFactory.Create()
                };

            var b = alternativeLexerFactory.Create(a);
            return new PathLexer(b);
        }
    }
}