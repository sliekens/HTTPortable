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

        public override StatusLine Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            StatusLine token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'status-line'");
        }

        public override bool TryRead(ITextScanner scanner, out StatusLine token)
        {
            if (scanner.EndOfInput)
            {
                token = default(StatusLine);
                return false;
            }

            var context = scanner.GetContext();
            HttpVersion httpVersion;
            if (!this.httpVersionLexer.TryRead(scanner, out httpVersion))
            {
                token = default(StatusLine);
                return false;
            }

            Space space1;
            if (!this.spaceLexer.TryRead(scanner, out space1))
            {
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLine);
                return false;
            }

            StatusCode statusCode;
            if (!this.statusCodeLexer.TryRead(scanner, out statusCode))
            {
                this.spaceLexer.PutBack(scanner, space1);
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLine);
                return false;
            }

            Space space2;
            if (!this.spaceLexer.TryRead(scanner, out space2))
            {
                this.statusCodeLexer.PutBack(scanner, statusCode);
                this.spaceLexer.PutBack(scanner, space1);
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLine);
                return false;
            }

            ReasonPhrase reasonPhrase;
            if (!this.reasonPhraseLexer.TryRead(scanner, out reasonPhrase))
            {
                this.spaceLexer.PutBack(scanner, space2);
                this.statusCodeLexer.PutBack(scanner, statusCode);
                this.spaceLexer.PutBack(scanner, space1);
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLine);
                return false;
            }

            EndOfLine endOfLine;
            if (!this.endOfLineLexer.TryRead(scanner, out endOfLine))
            {
                this.reasonPhraseLexer.PutBack(scanner, reasonPhrase);
                this.spaceLexer.PutBack(scanner, space2);
                this.statusCodeLexer.PutBack(scanner, statusCode);
                this.spaceLexer.PutBack(scanner, space1);
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLine);
                return false;
            }

            token = new StatusLine(httpVersion, space1, statusCode, space2, reasonPhrase, endOfLine, context);
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