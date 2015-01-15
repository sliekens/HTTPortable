namespace Http
{
    using System;
    using System.Diagnostics.Contracts;

    public class ResponseMessage : IResponseMessage
    {
        private readonly IHeaderCollection headers;

        public ResponseMessage()
            : this(new HeaderCollection())
        {
        }

        public ResponseMessage(IHeaderCollection headers)
        {
            Contract.Requires(headers != null);
            this.headers = headers;
        }

        public IHeaderCollection Headers
        {
            get
            {
                return this.headers;
            }
        }

        public string Reason { get; set; }
        public int Status { get; set; }
        public Version HttpVersion { get; set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.headers != null);
        }
    }
}