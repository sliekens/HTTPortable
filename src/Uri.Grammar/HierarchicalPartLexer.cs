namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using SLANG;

    

    public class HierarchicalPartLexer : Lexer<HierarchicalPart>
    {
        private readonly ILexer<Authority> authorityLexer;

        private readonly ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer;

        private readonly ILexer<PathAbsolute> pathAbsoluteLexer;

        private readonly ILexer<PathRootless> pathRootlessLexer;

        public HierarchicalPartLexer()
            : this(new AuthorityLexer(), new PathAbsoluteOrEmptyLexer(), new PathAbsoluteLexer(), new PathRootlessLexer())
        {
        }

        public HierarchicalPartLexer(ILexer<Authority> authorityLexer, ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer, ILexer<PathAbsolute> pathAbsoluteLexer, ILexer<PathRootless> pathRootlessLexer)
            : base("hier-part")
        {
            Contract.Requires(authorityLexer != null);
            Contract.Requires(pathAbsoluteOrEmptyLexer != null);
            Contract.Requires(pathAbsoluteLexer != null);
            Contract.Requires(pathRootlessLexer != null);
            this.authorityLexer = authorityLexer;
            this.pathAbsoluteOrEmptyLexer = pathAbsoluteOrEmptyLexer;
            this.pathAbsoluteLexer = pathAbsoluteLexer;
            this.pathRootlessLexer = pathRootlessLexer;
        }

        public override bool TryRead(ITextScanner scanner, out HierarchicalPart element)
        {
            if (scanner.EndOfInput)
            {
                element = default(HierarchicalPart);
                return false;
            }

            var context = scanner.GetContext();
            if (this.TryRead1(scanner, out element))
            {
                return true;
            }

            if (this.TryRead2(scanner, out element))
            {
                return true;
            }

            if (this.TryRead2(scanner, out element))
            {
                return true;
            }

            if (this.TryRead3(scanner, out element))
            {
                return true;
            }

            element = new HierarchicalPart(new PathEmpty(context), context);
            return true;
        }

        private bool TryRead1(ITextScanner scanner, out HierarchicalPart element)
        {
            if (scanner.EndOfInput)
            {
                element = default(HierarchicalPart);
                return false;
            }

            var context = scanner.GetContext();
            Element slashes;
            if (!TryReadTerminal(scanner, "//", out slashes))
            {
                element = default(HierarchicalPart);
                return false;
            }

            Authority authority;
            if (!this.authorityLexer.TryRead(scanner, out authority))
            {
                scanner.PutBack(slashes.Data);
                element = default(HierarchicalPart);
                return false;
            }

            PathAbsoluteOrEmpty path;
            if (!this.pathAbsoluteOrEmptyLexer.TryRead(scanner, out path))
            {
                scanner.PutBack(authority.Data);
                scanner.PutBack(slashes.Data);
                element = default(HierarchicalPart);
                return false;
            }

            element = new HierarchicalPart(slashes, authority, path, context);
            return true;
        }

        private bool TryRead2(ITextScanner scanner, out HierarchicalPart element)
        {
            if (scanner.EndOfInput)
            {
                element = default(HierarchicalPart);
                return false;
            }

            var context = scanner.GetContext();
            PathAbsolute path;
            if (this.pathAbsoluteLexer.TryRead(scanner, out path))
            {
                element = new HierarchicalPart(path, context);
                return true;
            }

            element = default(HierarchicalPart);
            return false;
        }

        private bool TryRead3(ITextScanner scanner, out HierarchicalPart element)
        {
            if (scanner.EndOfInput)
            {
                element = default(HierarchicalPart);
                return false;
            }

            var context = scanner.GetContext();
            PathRootless path;
            if (this.pathRootlessLexer.TryRead(scanner, out path))
            {
                element = new HierarchicalPart(path, context);
                return true;
            }

            element = default(HierarchicalPart);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.authorityLexer != null);
            Contract.Invariant(this.pathAbsoluteOrEmptyLexer != null);
            Contract.Invariant(this.pathAbsoluteLexer != null);
            Contract.Invariant(this.pathRootlessLexer != null);
        }
    }
}