namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using SLANG;

    

    public class RelativePartLexer : Lexer<RelativePart>
    {
        private readonly ILexer<Authority> authorityLexer;

        private readonly ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer;

        private readonly ILexer<PathAbsolute> pathAbsoluteLexer;

        private readonly ILexer<PathNoScheme> pathNoSchemeLexer;

        public RelativePartLexer()
            : this(new AuthorityLexer(), new PathAbsoluteOrEmptyLexer(), new PathAbsoluteLexer(), new PathNoSchemeLexer())
        {
        }

        public RelativePartLexer(ILexer<Authority> authorityLexer, ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer, ILexer<PathAbsolute> pathAbsoluteLexer, ILexer<PathNoScheme> pathNoSchemeLexer)
            : base("relative-part")
        {
            Contract.Requires(authorityLexer != null);
            Contract.Requires(pathAbsoluteOrEmptyLexer != null);
            Contract.Requires(pathAbsoluteLexer != null);
            Contract.Requires(pathNoSchemeLexer != null);
            this.authorityLexer = authorityLexer;
            this.pathAbsoluteOrEmptyLexer = pathAbsoluteOrEmptyLexer;
            this.pathAbsoluteLexer = pathAbsoluteLexer;
            this.pathNoSchemeLexer = pathNoSchemeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out RelativePart element)
        {
            if (scanner.EndOfInput)
            {
                element = default(RelativePart);
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

            element = new RelativePart(new PathEmpty(context), context);
            return true;
        }

        private bool TryRead1(ITextScanner scanner, out RelativePart element)
        {
            if (scanner.EndOfInput)
            {
                element = default(RelativePart);
                return false;
            }

            var context = scanner.GetContext();
            Element slashes;
            if (!TryReadTerminal(scanner, "//", out slashes))
            {
                element = default(RelativePart);
                return false;
            }

            Authority authority;
            if (!this.authorityLexer.TryRead(scanner, out authority))
            {
                scanner.PutBack(slashes.Data);
                element = default(RelativePart);
                return false;
            }

            PathAbsoluteOrEmpty path;
            if (!this.pathAbsoluteOrEmptyLexer.TryRead(scanner, out path))
            {
                scanner.PutBack(authority.Data);
                scanner.PutBack(slashes.Data);
                element = default(RelativePart);
                return false;
            }

            element = new RelativePart(slashes, authority, path, context);
            return true;
        }

        private bool TryRead2(ITextScanner scanner, out RelativePart element)
        {
            if (scanner.EndOfInput)
            {
                element = default(RelativePart);
                return false;
            }

            var context = scanner.GetContext();
            PathAbsolute path;
            if (this.pathAbsoluteLexer.TryRead(scanner, out path))
            {
                element = new RelativePart(path, context);
                return true;
            }

            element = default(RelativePart);
            return false;
        }

        private bool TryRead3(ITextScanner scanner, out RelativePart element)
        {
            if (scanner.EndOfInput)
            {
                element = default(RelativePart);
                return false;
            }

            var context = scanner.GetContext();
            PathNoScheme path;
            if (this.pathNoSchemeLexer.TryRead(scanner, out path))
            {
                element = new RelativePart(path, context);
                return true;
            }

            element = default(RelativePart);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.authorityLexer != null);
            Contract.Invariant(this.pathAbsoluteOrEmptyLexer != null);
            Contract.Invariant(this.pathAbsoluteLexer != null);
            Contract.Invariant(this.pathNoSchemeLexer != null);
            Contract.Invariant(this.pathEmptyLexer != null);
        }
    }
}
