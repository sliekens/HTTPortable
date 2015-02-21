using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class StatusLineToken : Element
    {
        private readonly HttpVersionToken httpVersion;
        private readonly ReasonPhraseToken reasonPhrase;
        private readonly StatusCodeToken statusCode;

        public StatusLineToken(HttpVersionToken httpVersion, Space space1, StatusCodeToken statusCode, Space space2,
            ReasonPhraseToken reasonPhrase, EndOfLine endOfLine, ITextContext context)
            : base(string.Concat(httpVersion, space1, statusCode, space2, reasonPhrase, endOfLine), context)
        {
            Contract.Requires(httpVersion != null);
            Contract.Requires(space1 != null);
            Contract.Requires(statusCode != null);
            Contract.Requires(space2 != null);
            Contract.Requires(reasonPhrase != null);
            Contract.Requires(endOfLine != null);
            this.httpVersion = httpVersion;
            this.statusCode = statusCode;
            this.reasonPhrase = reasonPhrase;
        }

        public HttpVersionToken HttpVersion
        {
            get
            {
                return this.httpVersion;
            }
        }

        public ReasonPhraseToken ReasonPhrase
        {
            get
            {
                return this.reasonPhrase;
            }
        }

        public StatusCodeToken StatusCode
        {
            get
            {
                return this.statusCode;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.httpVersion != null);
            Contract.Invariant(this.statusCode != null);
            Contract.Invariant(this.reasonPhrase != null);
        }
    }
}