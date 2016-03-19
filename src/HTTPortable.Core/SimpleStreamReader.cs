namespace Http
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Implements a <see cref="T:System.IO.TextReader"/> that reads characters from a byte stream. This reader is suitable for reading text transfer protocols that only use characters in the US ASCII set (e.g. HTTP).
    /// Unlike its framework counterpart, this reader does not support multibyte character encodings. This reader also does not buffer the stream internally. The implication for streams that support seeking is that the reader and the underlying stream do not require manual synchronization after seeking.
    /// For advanced scenarios, use the <see cref="T:System.IO.StreamReader"/> class instead.
    /// </summary>
    /// <remarks>
    /// The need for a simpler StreamReader class arose when it became apparent that the framework's StreamReader class is unsuitable for reading HTTP messages.
    /// The reason is that the framework's StreamReader class uses an internal buffer of at least 128 bytes.
    /// The StreamReader class always copies blocks of 128 bytes from the underlying stream to its internal buffer. This causes the reader to read beyond the end of HTTP message headers (unless the header's size coincidentally is a multiple of 128).
    /// </remarks>
    public sealed class SimpleStreamReader : TextReader
    {
        /// <summary>The default buffer size is 512KiB.</summary>
        public const long DefaultBufferSize = 524288;

        private readonly long bufferSize;
        private readonly Stream stream;
        private bool disposed;

        /// <summary>Initializes a new instance of the <see cref="M:Http.SimpleStreamReader.#ctor(System.IO.Stream)"/> class with a specified <see cref="T:System.IO.Stream"/> and a default buffer size.</summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream"/> to read.</param>
        public SimpleStreamReader(Stream stream)
            : this(stream, DefaultBufferSize)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="M:Http.SimpleStreamReader.#ctor(System.IO.Stream)"/> class with a specified <see cref="T:System.IO.Stream"/> and buffer size.</summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream"/> to read.</param>
        /// <param name="bufferSize">The buffer size to use for <see cref="ReadToEnd"/> and <see cref="ReadToEndAsync"/>.</param>
        public SimpleStreamReader(Stream stream, long bufferSize)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            }
            this.stream = stream;
            this.bufferSize = bufferSize;
        }

        /// <inheritdoc />
        public override int Peek()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            // Ensure that the stream supports seeking
            if (!this.stream.CanSeek)
            {
                return -1;
            }

            int octal;
            try
            {
                // Read the next byte
                octal = this.stream.ReadByte();
            }
            catch (NotSupportedException notSupportedException)
            {
                throw new IOException("An error occurred while reading from the underlying stream.", 
                    notSupportedException);
            }
            catch (ObjectDisposedException objectDisposedException)
            {
                throw new ObjectDisposedException("The underlying stream has already been closed.", 
                    objectDisposedException);
            }

            // Ensure that a byte was available
            if (octal == -1)
            {
                return -1;
            }

            try
            {
                // Rewind the stream
                this.stream.Seek(-1, SeekOrigin.Current);
            }
            catch (NotSupportedException notSupportedException)
            {
                // This block is unreachable if the stream is implemented correctly, because CanSeek was true
                throw new IOException("An error occurred while setting the position of the underlying stream.", 
                    notSupportedException);
            }
            catch (IOException ioException)
            {
                throw new IOException("An error occurred while setting the position of the underlying stream.", 
                    ioException);
            }
            catch (ObjectDisposedException objectDisposedException)
            {
                throw new ObjectDisposedException("The underlying stream has already been closed.", 
                    objectDisposedException);
            }

            // Return the character
            return (char)octal;
        }

        /// <inheritdoc />
        public override int Read()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            try
            {
                // Return the next byte
                return this.stream.ReadByte();
            }
            catch (NotSupportedException notSupportedException)
            {
                throw new IOException("An error occurred while reading from the underlying stream.", 
                    notSupportedException);
            }
            catch (ObjectDisposedException objectDisposedException)
            {
                throw new ObjectDisposedException("The underlying stream has already been closed.", 
                    objectDisposedException);
            }
        }

        /// <inheritdoc />
        public override int Read(char[] buffer, int index, int count)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            // Ensure that 'buffer' is not a null reference
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            // Ensure that 'index' is positive
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "The value of 'index' must be positive.");
            }

            // Ensure that 'count' is positive
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "The value of 'count' must be positive.");
            }

            // Ensure that the buffer can hold the requested number of characters
            if (buffer.Length < (index + count))
            {
                throw new ArgumentException(
                    "The buffer is too small to hold the requested number of characters starting at the given index", 
                    nameof(buffer));
            }

            var bytes = new byte[count];
            int byteCount;
            try
            {
                // Read up to the requested number of characters, assuming 1 byte per character.
                // TODO: what happens when the stream contains multibyte characters?
                byteCount = this.stream.Read(bytes, 0, count);
            }
            catch (NotSupportedException notSupportedException)
            {
                throw new IOException("An error occurred while reading from the underlying stream.", 
                    notSupportedException);
            }
            catch (IOException ioException)
            {
                throw new IOException("An error occurred while reading from the underlying stream.", ioException);
            }
            catch (ObjectDisposedException objectDisposedException)
            {
                throw new ObjectDisposedException("The underlying stream has already been closed.", 
                    objectDisposedException);
            }

            return Encoding.UTF8.GetChars(bytes, 0, byteCount, buffer, index);
        }

        /// <inheritdoc />
        public override async Task<int> ReadAsync(char[] buffer, int index, int count)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            // Ensure that 'buffer' is not a null reference
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            // Ensure that 'index' is positive
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "The value of 'index' must be positive.");
            }

            // Ensure that 'count' is positive
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "The value of 'count' must be positive.");
            }

            // Ensure that the buffer can hold the requested number of characters
            if (buffer.Length < (index + count))
            {
                throw new ArgumentException(
                    "The buffer is too small to hold the requested number of characters starting at the given index", 
                    nameof(buffer));
            }

            var bytes = new byte[count];
            int byteCount;
            try
            {
                // Read up to the requested number of characters, assuming 1 byte per character.
                // TODO: what happens when the stream contains multibyte characters?
                byteCount = await this.stream.ReadAsync(bytes, 0, count).ConfigureAwait(false);
            }
            catch (NotSupportedException notSupportedException)
            {
                throw new IOException("An error occurred while reading from the underlying stream.", 
                    notSupportedException);
            }
            catch (IOException ioException)
            {
                throw new IOException("An error occurred while reading from the underlying stream.", ioException);
            }
            catch (ObjectDisposedException objectDisposedException)
            {
                throw new ObjectDisposedException("The underlying stream has already been closed.", 
                    objectDisposedException);
            }

            return Encoding.UTF8.GetChars(bytes, 0, byteCount, buffer, index);
        }

        /// <inheritdoc />
        /// TODO: verify the correctness of this 'ReadBlock' implementation
        public override int ReadBlock(char[] buffer, int index, int count)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            var charCount = this.Read(buffer, index, count);
            if (charCount == 0)
            {
                return 0;
            }

            count -= charCount;
            if (count == 0)
            {
                return charCount;
            }

            index += charCount;
            return charCount + this.ReadBlock(buffer, index, count);
        }

        /// <inheritdoc />
        /// TODO: verify the correctness of this 'ReadBlockAsync' implementation
        public override async Task<int> ReadBlockAsync(char[] buffer, int index, int count)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            var charCount = await this.ReadAsync(buffer, index, count).ConfigureAwait(false);
            if (charCount == 0)
            {
                return 0;
            }

            count -= charCount;
            if (count == 0)
            {
                return charCount;
            }

            index += charCount;
            return charCount + await this.ReadBlockAsync(buffer, index, count).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string ReadLine()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            var buffer = new StringBuilder();
            bool encounteredCr = false;
            bool encounteredLf = false;
            int octal;
            while ((octal = this.Read()) != -1)
            {
                var c = (char)octal;
                if (c == '\r')
                {
                    if (encounteredCr)
                    {
                        buffer.Append(c);
                    }
                    else
                    {
                        encounteredCr = true;
                    }
                }
                else if (encounteredCr && c == '\n')
                {
                    encounteredLf = true;
                }
                else
                {
                    encounteredCr = false;
                    buffer.Append(c);
                }

                if (encounteredCr && encounteredLf)
                {
                    break;
                }
            }

            return buffer.ToString();
        }

        /// <inheritdoc />
        public override async Task<string> ReadLineAsync()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            var buffer = new char[1];
            var lineBuffer = new StringBuilder();
            bool encounteredCr = false;
            bool encounteredLf = false;
            while (0 != await this.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false))
            {
                var c = buffer[0];
                if (c == '\r')
                {
                    if (encounteredCr)
                    {
                        lineBuffer.Append(c);
                    }
                    else
                    {
                        encounteredCr = true;
                    }
                }
                else if (encounteredCr && c == '\n')
                {
                    encounteredLf = true;
                }
                else
                {
                    encounteredCr = false;
                    lineBuffer.Append(c);
                }

                if (encounteredCr && encounteredLf)
                {
                    break;
                }
            }

            return lineBuffer.ToString();
        }

        /// <inheritdoc />
        public override string ReadToEnd()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            var buffer = new char[this.bufferSize];
            using (var writer = new StringWriter())
            {
                int charCount;
                while (0 != (charCount = this.Read(buffer, 0, buffer.Length)))
                {
                    writer.Write(buffer, 0, charCount);
                }

                return writer.ToString();
            }
        }

        /// <inheritdoc />
        public override async Task<string> ReadToEndAsync()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            var buffer = new char[this.bufferSize];
            using (var writer = new StringWriter())
            {
                int charCount;
                while (0 != (charCount = await this.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)))
                {
                    await writer.WriteAsync(buffer, 0, charCount).ConfigureAwait(false);
                }

                return writer.ToString();
            }
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;
            base.Dispose(disposing);
        }
    }
}