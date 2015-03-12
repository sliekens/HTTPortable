namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class StartLineLexer : Lexer<StartLine>
    {
        private readonly ILexer<RequestLine> requestLineLexer;
        private readonly ILexer<StatusLine> statusLineLexer;

        public StartLineLexer(ILexer<RequestLine> requestLineLexer, ILexer<StatusLine> statusLineLexer)
            : base("start-line")
        {
            Contract.Requires(requestLineLexer != null);
            Contract.Requires(statusLineLexer != null);
            this.requestLineLexer = requestLineLexer;
            this.statusLineLexer = statusLineLexer;
        }

        public override bool TryRead(ITextScanner scanner, out StartLine element)
        {
            if (scanner.EndOfInput)
            {
                element = default(StartLine);
                return false;
            }

            var context = scanner.GetContext();
            RequestLine requestLine;
            if (this.requestLineLexer.TryRead(scanner, out requestLine))
            {
                element = new StartLine(requestLine, context);
                return true;
            }

            StatusLine statusLine;
            if (this.statusLineLexer.TryRead(scanner, out statusLine))
            {
                element = new StartLine(statusLine, context);
                return true;
            }

            element = default(StartLine);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.requestLineLexer != null);
            Contract.Invariant(this.statusLineLexer != null);
        }
    }
}