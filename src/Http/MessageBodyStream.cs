using System;
using System.IO;
using System.Linq;

namespace Http
{
    public class MessageBodyStream : Stream
    {
        private readonly Lazy<long> lazyLength;
        private readonly Stream responseContent;

        public MessageBodyStream(IResponseMessage response, Stream responseContent)
        {
            this.responseContent = responseContent;
            this.lazyLength = new Lazy<long>(() => ParseLength(response).GetValueOrDefault(-1));
        }

        public override void Flush()
        {
            responseContent.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            lock (this.responseContent)
            {
                var position = this.Position;
                var length = this.Length;
                var available = length - position;
                if (available == 0)
                {
                    return 0;
                }

                if (offset + count > available)
                {
                    count = (int)available;
                }

                var read = responseContent.Read(buffer, offset, count);
                this.Position += read;
                return read;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return responseContent.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            responseContent.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            responseContent.Write(buffer, offset, count);
        }

        public override bool CanRead
        {
            get { return responseContent.CanRead; }
        }

        public override bool CanSeek
        {
            get { return responseContent.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return responseContent.CanWrite; }
        }

        public override long Length
        {
            get
            {
                return this.lazyLength.Value;
            }
        }

        private static long? ParseLength(IResponseMessage response)
        {
            long length;
            var header = response.Headers.SingleOrDefault(h => h.Name.Equals("Content-Length"));
            if (header != null && long.TryParse(header.FirstOrDefault(), out length))
            {
                return length;
            }

            return null;
        }

        public override long Position { get; set; }
    }
}
