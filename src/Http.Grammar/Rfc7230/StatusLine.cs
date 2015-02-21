using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class StatusLine : Element
    {
        private readonly HttpVersion httpVersion;
        private readonly ReasonPhrase reasonPhrase;
        private readonly StatusCode statusCode;

        public StatusLine(HttpVersion httpVersion, Space space1, StatusCode statusCode, Space space2,
            ReasonPhrase reasonPhrase, EndOfLine endOfLine, ITextContext context)
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

        public HttpVersion HttpVersion
        {
            get
            {
                return this.httpVersion;
            }
        }

        public ReasonPhrase ReasonPhrase
        {
            get
            {
                return this.reasonPhrase;
            }
        }

        public StatusCode StatusCode
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