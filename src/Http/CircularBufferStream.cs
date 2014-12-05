using System;
using System.IO;

namespace Http
{
    public sealed class CircularBufferStream : Stream
    {
        private int head;
        private readonly byte[] buffer;
        private readonly int size;
        private readonly Stream stream;
        private int available;

        public CircularBufferStream(Stream stream)
            : this(stream, short.MaxValue)
        {
        }

        public CircularBufferStream(Stream stream, int bufferSize)
        {
            this.stream = stream;
            this.buffer = new byte[bufferSize];
            this.size = bufferSize;
            this.head = 0;
            this.available = 0;
        }

        public int Available
        {
            get
            {
                return available;
            }
        }

        public override bool CanRead
        {
            get
            {
                return this.stream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return this.stream.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return this.stream.CanWrite;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Available == 0;
            }
        }

        public bool IsFull
        {
            get
            {
                return Available == size;
            }
        }

        public override long Length
        {
            get
            {
                return this.stream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return this.stream.Position;
            }
            set
            {
                this.stream.Position = value;
            }
        }

        public void Discard()
        {
            if (Available == 0)
            {
                return;
            }

            head = (head + Available) % size;
            this.available = 0;
        }

        /// <summary>Fills the buffer with up to the given number of bytes.</summary>
        /// <param name="count">The number of bytes to buffer.</param>
        /// <returns>
        ///     The number of bytes in the buffer after the operation has completed. This value can be anywhere between 0 and
        ///     the given buffer size (both inclusive).
        /// </returns>
        public int FillBuffer(int count)
        {
            // (Performance) return early if the buffer is already full
            if (Available == size)
            {
                return Available;
            }

            // Ensure that the buffer will not overflow
            if (count > size)
            {
                count = size;
            }

            // Ensure that no buffered data will be overwritten
            if (Available > 0)
            {
                count -= Available;
            }

            var hasRead = 0;
            while (hasRead < count)
            {
                var maxRead = count - hasRead;
                var start = (head + Available + hasRead) % size;
                var pass = size - start;
                if (pass > maxRead)
                {
                    pass = maxRead;
                }

                var read = this.stream.Read(buffer, start, pass);
                if (read == 0)
                {
                    break;
                }

                hasRead += read;
            }

            this.available += hasRead;

            // Return the number of buffered bytes
            return this.Available;
        }

        public override void Flush()
        {
            this.stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var hasRead = 0;
            while (hasRead < count)
            {
                if (this.Available == 0)
                {
                    break;
                }

                var maxRead = count - hasRead;
                var pass = size - this.head;
                if (pass > maxRead)
                {
                    pass = maxRead;
                }

                if (pass > this.Available)
                {
                    pass = this.Available;
                }

                Buffer.BlockCopy(this.buffer, this.head, buffer, offset, pass);
                hasRead += pass;
                offset += pass;
                this.available -= pass;
                this.head = (this.head + pass) % this.size;
            }

            if (hasRead < count)
            {
                return this.stream.Read(buffer, offset, count - hasRead) + hasRead;
            }

            return hasRead;
        }

        public override int ReadByte()
        {
            if (this.Available == 0)
            {
                return this.stream.ReadByte();
            }

            var b = buffer[head];
            this.available--;
            this.head += 1;
            if (this.head == size)
            {
                this.head = 0;
            }

            return b;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.stream.Write(buffer, offset, count);
        }
    }
}