namespace Http.Grammars.Rfc7230
{
    using System.Diagnostics.Contracts;
    using Text.Scanning;
    using Text.Scanning.Core;

    public class StatusLineToken : Token
    {
        private readonly CrLfToken crLf;
        private readonly HttpVersionToken httpVersion;
        private readonly ReasonPhraseToken reasonPhrase;
        private readonly SpToken sp1;
        private readonly SpToken sp2;
        private readonly StatusCodeToken statusCode;

        public StatusLineToken(HttpVersionToken httpVersion, SpToken sp1, StatusCodeToken statusCode, SpToken sp2,
            ReasonPhraseToken reasonPhrase, CrLfToken crLf, ITextContext context)
            : base(string.Concat(httpVersion, sp1, statusCode, sp2, reasonPhrase, crLf), context)
        {
            Contract.Requires(httpVersion != null);
            Contract.Requires(sp1 != null);
            Contract.Requires(statusCode != null);
            Contract.Requires(sp2 != null);
            Contract.Requires(reasonPhrase != null);
            Contract.Requires(crLf != null);
            this.httpVersion = httpVersion;
            this.sp1 = sp1;
            this.statusCode = statusCode;
            this.sp2 = sp2;
            this.reasonPhrase = reasonPhrase;
            this.crLf = crLf;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.httpVersion != null);
            Contract.Invariant(this.sp1 != null);
            Contract.Invariant(this.statusCode != null);
            Contract.Invariant(this.sp2 != null);
            Contract.Invariant(this.reasonPhrase != null);
            Contract.Invariant(this.crLf != null);
        }
    }
}