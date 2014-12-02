using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Http
{
    public class ResponseMessage : IResponseMessage
    {
        private readonly Task<Stream> getResponseStreamDelegate;

        public ResponseMessage()
        {
            this.Headers = new GeneralHeaderCollection();
            this.ResponseHeaders = new ResponseHeaderCollection();
            this.ContentHeaders = new EntityHeaderCollection();
        }

        public IGeneralHeaderCollection Headers { get; private set; }

        public IEntityHeaderCollection ContentHeaders { get; set; }

        public Version Version { get; set; }

        public HttpStatusCode Status { get; set; }

        public string Reason { get; set; }

        public IResponseHeaderCollection ResponseHeaders { get; set; }
    }
}