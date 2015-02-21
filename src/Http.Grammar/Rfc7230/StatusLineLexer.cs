using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class StatusLineLexer : Lexer<StatusLineToken>
    {
        private readonly ILexer<EndOfLine> endOfLineLexer;
        private readonly ILexer<HttpVersionToken> httpVersionLexer;
        private readonly ILexer<ReasonPhraseToken> reasonPhraseLexer;
        private readonly ILexer<Space> spaceLexer;
        private readonly ILexer<StatusCodeToken> statusCodeLexer;

        public StatusLineLexer()
            : this(new HttpVersionLexer(), new SpaceLexer(), new StatusCodeLexer(), new ReasonPhraseLexer(), new EndOfLineLexer())
        {
        }

        public StatusLineLexer(ILexer<HttpVersionToken> httpVersionLexer, ILexer<Space> spaceLexer,
            ILexer<StatusCodeToken> statusCodeLexer, ILexer<ReasonPhraseToken> reasonPhraseLexer,
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

        public override StatusLineToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            StatusLineToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'status-line'");
        }

        public override bool TryRead(ITextScanner scanner, out StatusLineToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(StatusLineToken);
                return false;
            }

            var context = scanner.GetContext();
            HttpVersionToken httpVersion;
            if (!this.httpVersionLexer.TryRead(scanner, out httpVersion))
            {
                token = default(StatusLineToken);
                return false;
            }

            Space space1;
            if (!this.spaceLexer.TryRead(scanner, out space1))
            {
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLineToken);
                return false;
            }

            StatusCodeToken statusCode;
            if (!this.statusCodeLexer.TryRead(scanner, out statusCode))
            {
                this.spaceLexer.PutBack(scanner, space1);
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLineToken);
                return false;
            }

            Space space2;
            if (!this.spaceLexer.TryRead(scanner, out space2))
            {
                this.statusCodeLexer.PutBack(scanner, statusCode);
                this.spaceLexer.PutBack(scanner, space1);
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLineToken);
                return false;
            }

            ReasonPhraseToken reasonPhrase;
            if (!this.reasonPhraseLexer.TryRead(scanner, out reasonPhrase))
            {
                this.spaceLexer.PutBack(scanner, space2);
                this.statusCodeLexer.PutBack(scanner, statusCode);
                this.spaceLexer.PutBack(scanner, space1);
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLineToken);
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
                token = default(StatusLineToken);
                return false;
            }

            token = new StatusLineToken(httpVersion, space1, statusCode, space2, reasonPhrase, endOfLine, context);
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