namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class StatusLineLexer : SequenceLexer<StatusLine, HttpVersion, Space, StatusCode, Space, ReasonPhrase, EndOfLine>
    {
        private readonly ILexer<EndOfLine> endOfLineLexer;
        private readonly ILexer<HttpVersion> httpVersionLexer;
        private readonly ILexer<ReasonPhrase> reasonPhraseLexer;
        private readonly ILexer<Space> spaceLexer;
        private readonly ILexer<StatusCode> statusCodeLexer;

        public StatusLineLexer()
            : this(
                new HttpVersionLexer(), new SpaceLexer(), new StatusCodeLexer(), new ReasonPhraseLexer(),
                new EndOfLineLexer())
        {
        }

        public StatusLineLexer(ILexer<HttpVersion> httpVersionLexer, ILexer<Space> spaceLexer,
            ILexer<StatusCode> statusCodeLexer, ILexer<ReasonPhrase> reasonPhraseLexer,
            ILexer<EndOfLine> endOfLineLexer)
            : base("status-line")
        {
            Contract.Requires(httpVersionLexer != null);
            Contract.Requires(spaceLexer != null);
            Contract.Requires(statusCodeLexer != null);
            Contract.Requires(reasonPhraseLexer != null);
            Contract.Requires(endOfLineLexer != null);
            this.httpVersionLexer = httpVersionLexer;
            this.spaceLexer = spaceLexer;
            this.statusCodeLexer = statusCodeLexer;
            this.reasonPhraseLexer = reasonPhraseLexer;
            this.endOfLineLexer = endOfLineLexer;
        }

        protected override StatusLine CreateInstance(
            HttpVersion element1,
            Space element2,
            StatusCode element3,
            Space element4,
            ReasonPhrase element5,
            EndOfLine element6,
            ITextContext context)
        {
            return new StatusLine(element1, element2, element3, element4, element5, element6, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out HttpVersion element)
        {
            return this.httpVersionLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out Space element)
        {
            return this.spaceLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead3(ITextScanner scanner, out StatusCode element)
        {
            return this.statusCodeLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead4(ITextScanner scanner, out Space element)
        {
            return this.spaceLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead5(ITextScanner scanner, out ReasonPhrase element)
        {
            return this.reasonPhraseLexer.TryRead(scanner, out element);
        }

        protected override bool TryRead6(ITextScanner scanner, out EndOfLine element)
        {
            return this.endOfLineLexer.TryRead(scanner, out element);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.httpVersionLexer != null);
            Contract.Invariant(this.spaceLexer != null);
            Contract.Invariant(this.statusCodeLexer != null);
            Contract.Invariant(this.reasonPhraseLexer != null);
            Contract.Invariant(this.endOfLineLexer != null);
        }
    }
}