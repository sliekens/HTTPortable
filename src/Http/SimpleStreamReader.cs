using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Http
{
    public sealed class SimpleStreamReader : TextReader
    {
        private readonly Stream stream;
        private char previousChar;
        private bool peeked;

        public SimpleStreamReader(Stream stream)
        {
            Contract.Requires(stream != null);
            this.stream = stream;
        }


        public override int Read()
        {
            var octal = this.stream.ReadByte();
            if (octal == -1)
            {
                return -1;
            }

            // Reset the buffer status
            if (this.peeked)
            {
                this.peeked = false;
            }

            return octal;
        }

        public override int Peek()
        {
            // Ensure that the reader won't look past the next character when Peek() is called multiple times
            if (this.peeked)
            {
                return this.previousChar;
            }

            // Throw an exception for streams that do not support seeking
            if (!this.stream.CanSeek)
            {
                throw new NotSupportedException("The underlying stream does not support seeking.");
            }

            // Read the next byte
            var octal = this.stream.ReadByte();

            // Ensure that a byte was available
            if (octal == -1)
            {
                return -1;
            }

            // Rewind the stream    
            this.stream.Seek(-1, SeekOrigin.Current);

            // Update the buffer and its status
            this.previousChar = (char)octal;
            this.peeked = true;

            // Return the buffered character
            return this.previousChar;
        }

        public override int Read(char[] buffer, int index, int count)
        {
            if (count > buffer.Length)
            {
                count = buffer.Length;
            }

            if (index > 0)
            {
                count -= index;
            }

            var bytes = new byte[count];
            var byteCount = this.stream.Read(bytes, 0, count);
            var charCount = Encoding.UTF8.GetChars(bytes, 0, byteCount, buffer, index);

            // Reset the buffer status
            if (this.peeked && charCount >= 1)
            {
                this.peeked = false;
            }

            return charCount;
        }

        public override async Task<int> ReadAsync(char[] buffer, int index, int count)
        {
            if (count > buffer.Length)
            {
                count = buffer.Length;
            }

            if (index > 0)
            {
                count -= index;
            }

            var bytes = new byte[count];
            var byteCount = await this.stream.ReadAsync(bytes, 0, count).ConfigureAwait(false);
            var charCount = Encoding.UTF8.GetChars(bytes, 0, byteCount, buffer, index);

            // Reset the buffer status
            if (this.peeked && charCount >= 1)
            {
                this.peeked = false;
            }

            return charCount;
        }

        public override string ReadLine()
        {
            var buffer = new char[1];
            var lineBuffer = new StringBuilder();
            bool encounteredCr = false;
            bool encounteredLf = false;
            while (0 != this.Read(buffer, 0, buffer.Length))
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

        public override async Task<string> ReadLineAsync()
        {
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

        public override string ReadToEnd()
        {
            var buffer = new char[4096];
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

        public override async Task<string> ReadToEndAsync()
        {
            var buffer = new char[4096];
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

        public override int ReadBlock(char[] buffer, int index, int count)
        {
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

        public override async Task<int> ReadBlockAsync(char[] buffer, int index, int count)
        {
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

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.stream != null);
        }
    }
}
