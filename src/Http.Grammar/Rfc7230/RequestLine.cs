using System;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class RequestLine : Element
    {
        private readonly Method method;
        private readonly Token requestTarget;
        private readonly HttpVersion httpVersion;

        // TODO: implement a URI element
        public RequestLine(Method method, Space sp1, Token requestTarget, Space sp2, HttpVersion httpVersion, EndOfLine endOfLine, ITextContext context)
            : base(string.Concat(method.Data, sp1.Data, requestTarget.Data, sp2.Data, httpVersion.Data, endOfLine.Data), context)
        {
            Contract.Requires(method != null);
            Contract.Requires(sp1 != null);
            Contract.Requires(requestTarget != null);
            Contract.Requires(sp2 != null);
            Contract.Requires(httpVersion != null);
            Contract.Requires(endOfLine != null);
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
