using System;
using System.IO;
using System.Threading.Tasks;

namespace Http
{
    using System.Diagnostics.Contracts;

    public class ResponseMessage : IResponseMessage
    {
        private readonly Task<Stream> getResponseStreamDelegate;
        private readonly IHeaderCollection headers;

        public ResponseMessage()
        {
            this.headers = new HeaderCollection();
        }

        public IHeaderCollection Headers
        {
            get
            {
                return this.headers;
            }
        }

        public Version Version { get; set; }

        public int Status { get; set; }

        public string Reason { get; set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.headers != null);
        }
    }
}