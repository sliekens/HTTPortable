namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class PathAbsoluteLexer : Lexer<PathAbsolute>
    {
        private readonly ILexer<PathRootless> pathRootlessLexer;

        public PathAbsoluteLexer()
            : this(new PathRootlessLexer())
        {
        }

        public PathAbsoluteLexer(ILexer<PathRootless> pathRootlessLexer)
            : base("path-absolute")
        {
            Contract.Requires(pathRootlessLexer != null);
            this.pathRootlessLexer = pathRootlessLexer;
        }

        public override bool TryRead(ITextScanner scanner, out PathAbsolute element)
        {
            if (scanner.EndOfInput)
            {
                element = default(PathAbsolute);
                return false;
            }

            var context = scanner.GetContext();
            if (!scanner.TryMatch('/'))
            {
                element = default(PathAbsolute);
                return false;
            }

            PathRootless path;
            if (this.pathRootlessLexer.TryRead(scanner, out path))
            {
                element = new PathAbsolute(path, context);
            }
            else
            {
                element = new PathAbsolute(context);
            }

            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.pathRootlessLexer != null);
        }
    }
}