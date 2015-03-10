namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using Uri.Grammar;

    public class OriginFormLexer : Lexer<OriginForm>
    {
        private readonly ILexer<AbsolutePath> absolutePathLexer;
        private readonly ILexer<Query> queryLexer;

        public OriginFormLexer()
            : this(new AbsolutePathLexer(), new QueryLexer())
        {
        }

        public OriginFormLexer(ILexer<AbsolutePath> absolutePathLexer, ILexer<Query> queryLexer)
            : base("origin-form")
        {
            Contract.Requires(absolutePathLexer != null);
            Contract.Requires(queryLexer != null);
            this.absolutePathLexer = absolutePathLexer;
            this.queryLexer = queryLexer;
        }

        public override bool TryRead(ITextScanner scanner, out OriginForm element)
        {
            if (scanner.EndOfInput)
            {
                element = default(OriginForm);
                return false;
            }

            var context = scanner.GetContext();
            AbsolutePath absolutePath;
            if (!this.absolutePathLexer.TryRead(scanner, out absolutePath))
            {
                element = default(OriginForm);
                return false;
            }

            Element querySeparator;
            if (!TryReadTerminal(scanner, '?', out querySeparator))
            {
                element = new OriginForm(absolutePath, null, null, context);
                return true;
            }

            Query query;
            if (!this.queryLexer.TryRead(scanner, out query))
            {
                scanner.PutBack(querySeparator.Data);
            }

            element = new OriginForm(absolutePath, querySeparator, query, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.absolutePathLexer != null);
            Contract.Invariant(this.queryLexer != null);
        }
    }
}