namespace Http
{
    using System;
    using System.IO;
    using System.Threading;

    public class MessageBodyStream : Stream
    {
        private readonly long contentLength;
        private readonly Stream messageBody;
        private bool disposed;
        private long position;

        public MessageBodyStream(Stream messageBody, long contentLength)
        {
            if (messageBody == null)
            {
                throw new ArgumentNullException(nameof(messageBody));
            }
            this.messageBody = messageBody;
            this.contentLength = contentLength;
        }

        /// <inheritdoc />
        public override bool CanRead => messageBody.CanRead;

        /// <inheritdoc />
        public override bool CanSeek => messageBody.CanSeek;

        /// <inheritdoc />
        public override bool CanWrite => messageBody.CanWrite;

        /// <inheritdoc />
        public override long Length => contentLength;

        /// <inheritdoc />
        public override long Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        /// <inheritdoc />
        public override void Flush()
        {
            try
            {
                messageBody.Flush();
            }
            catch (IOException ioException)
            {
                throw new IOException("An I/O error occurred while flushing the stream.", ioException);
            }
        }

        /// <inheritdoc />
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer), "Precondition: buffer != null");
            }

            if ((offset + count) > buffer.Length)
            {
                throw new ArgumentException("Precondition: buffer.Length >= (offset + count)");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), offset, "Precondition: offset >= 0");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Precondition: count >= 0");
            }

            if (!CanRead)
            {
                throw new NotSupportedException("Precondition: Stream.CanRead");
            }

            // Optimization: return early if the caller wants 0 bytes
            if (count == 0)
            {
                return 0;
            }


            lock (messageBody)
            {
                var unread = contentLength - position;

                // Optimization: return early if there are no unread bytes
                if (unread == 0)
                {
                    return 0;
                }

                // Ensure no reading past the end of the message body
                // This step is vital: HTTP pipelining permits multiple messages on the same stream
                if (count > unread)
                {
                    count = (int)unread;
                }

                // Delegate the read operation to the inner stream
                int bytesRead;
                try
                {
                    bytesRead = messageBody.Read(buffer, offset, count);
                }
                catch (IOException ioException)
                {
                    throw new IOException("An I/O error occurred while reading the stream.", ioException);
                }

                // Update the position
                Interlocked.Add(ref position, bytesRead);

                // Return the number of bytes read
                return bytesRead;
            }
        }

        /// <inheritdoc />
        public override long Seek(long offset, SeekOrigin origin)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            if (!CanSeek)
            {
                throw new NotSupportedException("Precondition: Stream.CanSeek");
            }

            try
            {
                return messageBody.Seek(offset, origin);
            }
            catch (IOException ioException)
            {
                throw new IOException("An I/O error occurred while setting the position of the stream.", ioException);
            }
        }

        /// <inheritdoc />
        public override void SetLength(long value)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            if (!CanWrite)
            {
                throw new NotSupportedException("Precondition: Stream.CanWrite");
            }

            if (!CanSeek)
            {
                throw new NotSupportedException("Precondition: Stream.CanSeek");
            }

            try
            {
                messageBody.SetLength(value);
            }
            catch (IOException ioException)
            {
                throw new IOException("An I/O error occurred while setting the length of the stream.", ioException);
            }
        }

        /// <inheritdoc />
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer), "Precondition: buffer != null");
            }

            if ((offset + count) > buffer.Length)
            {
                throw new ArgumentException("Precondition: buffer.Length >= (offset + count)");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), offset, "Precondition: offset >= 0");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Precondition: count >= 0");
            }

            if (!CanWrite)
            {
                throw new NotSupportedException("Precondition: Stream.CanWrite");
            }

            lock (messageBody)
            {
                var bytesToWrite = contentLength - position;
                if (bytesToWrite == 0 || count > bytesToWrite)
                {
                    throw new IOException("Attempt to write past the given Content-Length");
                }

                try
                {
                    messageBody.Write(buffer, offset, count);
                }
                catch (IOException ioException)
                {
                    throw new IOException("An I/O error occurred while writing to the stream.", ioException);
                }

                Interlocked.Add(ref position, count);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            disposed = true;
        }
    }
}