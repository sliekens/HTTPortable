namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using SLANG;

    

    public class PathLexer : Lexer<Path>
    {
        private readonly ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer;

        private readonly ILexer<PathAbsolute> pathAbsoluteLexer;

        private readonly ILexer<PathNoScheme> pathNoSchemeLexer;

        private readonly ILexer<PathRootless> pathRootlessLexer;

        public PathLexer()
            : this(new PathAbsoluteOrEmptyLexer(), new PathAbsoluteLexer(), new PathNoSchemeLexer(), new PathRootlessLexer())
        {
        }

        public PathLexer(ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer, ILexer<PathAbsolute> pathAbsoluteLexer, ILexer<PathNoScheme> pathNoSchemeLexer, ILexer<PathRootless> pathRootlessLexer)
            : base("path")
        {
            Contract.Requires(pathAbsoluteOrEmptyLexer != null);
            Contract.Requires(pathAbsoluteLexer != null);
            Contract.Requires(pathNoSchemeLexer != null);
            Contract.Requires(pathRootlessLexer != null);
            this.pathAbsoluteOrEmptyLexer = pathAbsoluteOrEmptyLexer;
            this.pathAbsoluteLexer = pathAbsoluteLexer;
            this.pathNoSchemeLexer = pathNoSchemeLexer;
            this.pathRootlessLexer = pathRootlessLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Path element)
        {
            var context = scanner.GetContext();
            if (scanner.EndOfInput)
            {
                element = new Path(new PathEmpty(context), context);
                return true;
            }

            PathAbsoluteOrEmpty pathAbsoluteOrEmpty;
            if (this.pathAbsoluteOrEmptyLexer.TryRead(scanner, out pathAbsoluteOrEmpty))
            {
                element = new Path(pathAbsoluteOrEmpty, context);
                return true;
            }

            PathNoScheme pathNoScheme;
            if (this.pathNoSchemeLexer.TryRead(scanner, out pathNoScheme))
            {
                element = new Path(pathNoScheme, context);
                return true;
            }

            PathRootless pathRootless;
            if (this.pathRootlessLexer.TryRead(scanner, out pathRootless))
            {
                element = new Path(pathRootless, context);
                return true;
            }

            element = default(Path);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.pathAbsoluteOrEmptyLexer != null);
            Contract.Invariant(this.pathAbsoluteLexer != null);
            Contract.Invariant(this.pathNoSchemeLexer != null);
            Contract.Invariant(this.pathRootlessLexer != null);
        }
    }
}
