using System;
using System.IO;

namespace Http
{
    public class MessageBodyStream : Stream
    {
        private readonly bool isRequest;
        private readonly bool isResponse;
        private readonly Lazy<long> lazyLength;
        private readonly Stream messageBody;

        public MessageBodyStream(IResponseMessage responseMessage, Stream messageBody)
        {
            this.isResponse = true;
            this.messageBody = messageBody;
            this.lazyLength = new Lazy<long>(() =>
            {
                long result;
                var headers = responseMessage.Headers;
                if (headers != null && headers.TryGetContentLength(out result))
                {
                    return result;
                }

                return 0;
            });
        }

        public MessageBodyStream(IRequestMessage requestMessage, Stream messageBody)
        {
            this.isRequest = true;
            this.messageBody = messageBody;
            this.lazyLength = new Lazy<long>(() =>
            {
                long result;
                var headers = requestMessage.Headers;
                if (headers != null && headers.TryGetContentLength(out result))
                {
                    return result;
                }

                return 0;
            });
            
        }

        public override void Flush()
        {
            this.messageBody.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!this.CanRead)
            {
                throw new NotSupportedException();
            }

            if (count == 0)
            {
                return 0;
            }

            lock (this.messageBody)
            {
                var position = this.Position;
                var length = this.Length;
                var available = length - position;

                // Check for end of input
                if (available == 0)
                {
                    return 0;
                }

                // Ensure no reading past end of input
                if (count > available)
                {
                    count = (int)available;
                }

                // Ensure buffer index in range
                if (offset + count > buffer.Length)
                {
                    count = buffer.Length - offset;
                }

                // Delegate read operation to the inner stream
                var read = this.messageBody.Read(buffer, offset, count);

                // Update the position
                this.Position += read;

                // Return the number of bytes read
                return read;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.messageBody.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.messageBody.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (!this.CanWrite)
            {
                throw new NotSupportedException();
            }

            lock (this.messageBody)
            {
                var position = this.Position;
                var length = this.Length;
                var bytesToWrite = length - position;
                if (bytesToWrite == 0 || count > bytesToWrite)
                {
                    throw new InvalidOperationException("Attempt to write past the given Content-Length");
                }

                this.messageBody.Write(buffer, offset, count);
            }
        }

        public override bool CanRead
        {
            get { return this.isResponse && this.messageBody.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.messageBody.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return this.isRequest && this.messageBody.CanWrite; }
        }

        public override long Length
        {
            get
            {
                return this.lazyLength.Value;
            }
        }

        public override long Position { get; set; }
    }
}
