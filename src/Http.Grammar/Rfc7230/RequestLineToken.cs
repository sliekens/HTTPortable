using System;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class RequestLineToken : Token
    {
        private readonly MethodToken method;
        private readonly TokenToken requestTarget;
        private readonly HttpVersionToken httpVersion;

        // TODO: implement a URI token
        public RequestLineToken(MethodToken method, SpToken sp1, TokenToken requestTarget, SpToken sp2, HttpVersionToken httpVersion, CrLfToken crLf, ITextContext context)
            : base(string.Concat(method.Data, sp1.Data, requestTarget.Data, sp2.Data, httpVersion.Data, crLf.Data), context)
        {
            Contract.Requires(method != null);
            Contract.Requires(sp1 != null);
            Contract.Requires(requestTarget != null);
            Contract.Requires(sp2 != null);
            Contract.Requires(httpVersion != null);
            Contract.Requires(crLf != null);
            this.method = method;
            this.requestTarget = requestTarget;
            this.httpVersion = httpVersion;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.method != null);
            Contract.Invariant(this.requestTarget != null);
            Contract.Invariant(this.httpVersion != null);
        }
    }
}
