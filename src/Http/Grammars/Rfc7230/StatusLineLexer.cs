namespace Http.Grammars.Rfc7230
{
    using System.Diagnostics.Contracts;
    using Text.Scanning;
    using Text.Scanning.Core;

    public class StatusLineLexer : Lexer<StatusLineToken>
    {
        private readonly ILexer<CrLfToken> crLfLexer;
        private readonly ILexer<HttpVersionToken> httpVersionLexer;
        private readonly ILexer<ReasonPhraseToken> reasonPhraseLexer;
        private readonly ILexer<SpToken> spLexer;
        private readonly ILexer<StatusCodeToken> statusCodeLexer;

        public StatusLineLexer(ILexer<HttpVersionToken> httpVersionLexer, ILexer<SpToken> spLexer,
            ILexer<StatusCodeToken> statusCodeLexer, ILexer<ReasonPhraseToken> reasonPhraseLexer,
            ILexer<CrLfToken> crLfLexer)
        {
            Contract.Requires(httpVersionLexer != null);
            Contract.Requires(spLexer != null);
            Contract.Requires(statusCodeLexer != null);
            Contract.Requires(reasonPhraseLexer != null);
            Contract.Requires(crLfLexer != null);
            this.httpVersionLexer = httpVersionLexer;
            this.spLexer = spLexer;
            this.statusCodeLexer = statusCodeLexer;
            this.reasonPhraseLexer = reasonPhraseLexer;
            this.crLfLexer = crLfLexer;
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

            SpToken sp1;
            if (!this.spLexer.TryRead(scanner, out sp1))
            {
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLineToken);
                return false;
            }

            StatusCodeToken statusCode;
            if (!this.statusCodeLexer.TryRead(scanner, out statusCode))
            {
                this.spLexer.PutBack(scanner, sp1);
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLineToken);
                return false;
            }

            SpToken sp2;
            if (!this.spLexer.TryRead(scanner, out sp2))
            {
                this.statusCodeLexer.PutBack(scanner, statusCode);
                this.spLexer.PutBack(scanner, sp1);
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLineToken);
                return false;
            }

            ReasonPhraseToken reasonPhrase;
            if (!this.reasonPhraseLexer.TryRead(scanner, out reasonPhrase))
            {
                this.spLexer.PutBack(scanner, sp2);
                this.statusCodeLexer.PutBack(scanner, statusCode);
                this.spLexer.PutBack(scanner, sp1);
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLineToken);
                return false;
            }

            CrLfToken crLf;
            if (!this.crLfLexer.TryRead(scanner, out crLf))
            {
                this.reasonPhraseLexer.PutBack(scanner, reasonPhrase);
                this.spLexer.PutBack(scanner, sp2);
                this.statusCodeLexer.PutBack(scanner, statusCode);
                this.spLexer.PutBack(scanner, sp1);
                this.httpVersionLexer.PutBack(scanner, httpVersion);
                token = default(StatusLineToken);
                return false;
            }

            token = new StatusLineToken(httpVersion, sp1, statusCode, sp2, reasonPhrase, crLf, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.httpVersionLexer != null);
            Contract.Invariant(this.spLexer != null);
            Contract.Invariant(this.statusCodeLexer != null);
            Contract.Invariant(this.reasonPhraseLexer != null);
            Contract.Invariant(this.crLfLexer != null);
        }
    }
}