using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class StatusLineLexer : Lexer<StatusLine>
    {
        private readonly ILexer<EndOfLine> endOfLineLexer;
        private readonly ILexer<HttpVersion> httpVersionLexer;
        private readonly ILexer<ReasonPhrase> reasonPhraseLexer;
        private readonly ILexer<Space> spaceLexer;
        private readonly ILexer<StatusCode> statusCodeLexer;

        public StatusLineLexer()
            : this(new HttpVersionLexer(), new SpaceLexer(), new StatusCodeLexer(), new ReasonPhraseLexer(), new EndOfLineLexer())
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

        public override bool TryRead(ITextScanner scanner, out StatusLine element)
        {
            if (scanner.EndOfInput)
            {
                element = default(StatusLine);
                return false;
            }

            var context = scanner.GetContext();
            HttpVersion httpVersion;
            if (!this.httpVersionLexer.TryRead(scanner, out httpVersion))
            {
                element = default(StatusLine);
                return false;
            }

            Space space1;
            if (!this.spaceLexer.TryRead(scanner, out space1))
            {
                scanner.PutBack(httpVersion.Data);
                element = default(StatusLine);
                return false;
            }

            StatusCode statusCode;
            if (!this.statusCodeLexer.TryRead(scanner, out statusCode))
            {
                scanner.PutBack(space1.Data);
                scanner.PutBack(httpVersion.Data);
                element = default(StatusLine);
                return false;
            }

            Space space2;
            if (!this.spaceLexer.TryRead(scanner, out space2))
            {
                scanner.PutBack(statusCode.Data);
                scanner.PutBack(space1.Data);
                scanner.PutBack(httpVersion.Data);
                element = default(StatusLine);
                return false;
            }

            ReasonPhrase reasonPhrase;
            if (!this.reasonPhraseLexer.TryRead(scanner, out reasonPhrase))
            {
                scanner.PutBack(space2.Data);
                scanner.PutBack(statusCode.Data);
                scanner.PutBack(space1.Data);
                scanner.PutBack(httpVersion.Data);
                element = default(StatusLine);
                return false;
            }

            EndOfLine endOfLine;
            if (!this.endOfLineLexer.TryRead(scanner, out endOfLine))
            {
                scanner.PutBack(reasonPhrase.Data);
                scanner.PutBack(space2.Data);
                scanner.PutBack(statusCode.Data);
                scanner.PutBack(space1.Data);
                scanner.PutBack(httpVersion.Data);
                element = default(StatusLine);
                return false;
            }

            element = new StatusLine(httpVersion, space1, statusCode, space2, reasonPhrase, endOfLine, context);
            return true;
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