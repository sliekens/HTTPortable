using System;
using System.IO;

namespace Http
{
    using System.Diagnostics.Contracts;
    using System.Threading;

    public class MessageBodyStream : Stream
    {
        private readonly Stream messageBody;
        private readonly long contentLength;
        private long position;
        private bool disposed;

        public MessageBodyStream(Stream messageBody, long contentLength)
        {
            Contract.Requires(messageBody != null);
            Contract.Requires(messageBody.Length >= 0);
            this.messageBody = messageBody;
            this.contentLength = contentLength;
        }

        /// <inheritdoc />
        public override void Flush()
        {
            try
            {
                this.messageBody.Flush();
            }
            catch (IOException ioException)
            {
                throw new IOException("An I/O error occurred while flushing the stream.", ioException);
            }
        }

        /// <inheritdoc />
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            if (buffer == null)
            {
                throw new ArgumentNullException("buffer", "Precondition: buffer != null");
            }

            if ((offset + count) > buffer.Length)
            {
                throw new ArgumentException("Precondition: buffer.Length >= (offset + count)");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", offset, "Precondition: offset >= 0");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", count, "Precondition: count >= 0");
            }

            if (!this.CanRead)
            {
                throw new NotSupportedException("Precondition: Stream.CanRead");
            }

            lock (this.messageBody)
            {
                var position = this.Position;
                var length = this.Length;
                var available = length - position;
                int bytesRead;

                // Ensure no reading past the end of the message body
                // This step is vital: HTTP pipelining permits multiple messages on the same stream
                if (count > available)
                {
                    count = (int)available;
                }

                // Delegate read operation to the inner stream
                try
                {
                    bytesRead = this.messageBody.Read(buffer, offset, count);
                }
                catch (IOException ioException)
                {
                    throw new IOException("An I/O error occurred while reading the stream.", ioException);
                }

                // Update the position
                Interlocked.Add(ref this.position, bytesRead);

                // Return the number of bytes read
                return bytesRead;
            }
        }

        /// <inheritdoc />
        public override long Seek(long offset, SeekOrigin origin)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            if (!this.CanSeek)
            {
                throw new NotSupportedException("Precondition: Stream.CanSeek");
            }

            try
            {
                return this.messageBody.Seek(offset, origin);
            }
            catch (IOException ioException)
            {
                throw new IOException("An I/O error occurred while setting the position of the stream.", ioException);
            }
        }

        /// <inheritdoc />
        public override void SetLength(long value)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            if (!this.CanWrite)
            {
                throw new NotSupportedException("Precondition: Stream.CanWrite");
            }

            if (!this.CanSeek)
            {
                throw new NotSupportedException("Precondition: Stream.CanSeek");
            }

            try
            {
                this.messageBody.SetLength(value);
            }
            catch (IOException ioException)
            {
                throw new IOException("An I/O error occurred while setting the length of the stream.", ioException);
            }
        }

        /// <inheritdoc />
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

             if (buffer == null)
            {
                throw new ArgumentNullException("buffer", "Precondition: buffer != null");
            }

            if ((offset + count) > buffer.Length)
            {
                throw new ArgumentException("Precondition: buffer.Length >= (offset + count)");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", offset, "Precondition: offset >= 0");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", count, "Precondition: count >= 0");
            }

            if (!this.CanWrite)
            {
                throw new NotSupportedException("Precondition: Stream.CanWrite");
            }

            lock (this.messageBody)
            {
                var bytesToWrite = this.contentLength - this.position;
                if (bytesToWrite == 0 || count > bytesToWrite)
                {
                    throw new IOException("Attempt to write past the given Content-Length");
                }

                try
                {
                    this.messageBody.Write(buffer, offset, count);
                }
                catch (IOException ioException)
                {
                    throw new IOException("An I/O error occurred while writing to the stream.", ioException);
                }

                Interlocked.Add(ref this.position, count);
            }
        }

        /// <inheritdoc />
        public override bool CanRead
        {
            get { return this.messageBody.CanRead; }
        }

        /// <inheritdoc />
        public override bool CanSeek
        {
            get { return this.messageBody.CanSeek; }
        }

        /// <inheritdoc />
        public override bool CanWrite
        {
            get { return this.messageBody.CanWrite; }
        }

        /// <inheritdoc />
        public override long Length
        {
            get
            {
                return this.contentLength;
            }
        }

        /// <inheritdoc />
        public override long Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this.disposed = true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.messageBody != null);
            Contract.Invariant(this.contentLength >= 0);
        }
    }
}
