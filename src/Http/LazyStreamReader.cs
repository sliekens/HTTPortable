using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Http
{
    public sealed class LazyStreamReader : StreamReader
    {
        public LazyStreamReader(Stream stream) : base(stream)
        {
        }

        public LazyStreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen) : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen)
        {
        }

        public LazyStreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize) : base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize)
        {
        }

        public LazyStreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks) : base(stream, encoding, detectEncodingFromByteOrderMarks)
        {
        }

        public LazyStreamReader(Stream stream, Encoding encoding) : base(stream, encoding)
        {
        }

        public LazyStreamReader(Stream stream, bool detectEncodingFromByteOrderMarks) : base(stream, detectEncodingFromByteOrderMarks)
        {
        }

        public override Task<int> ReadAsync(char[] buffer, int index, int count)
        {
            return base.ReadAsync(buffer, index, count);
        }

        public override int ReadBlock(char[] buffer, int index, int count)
        {
            return base.ReadBlock(buffer, index, count);
        }

        public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
        {
            return base.ReadBlockAsync(buffer, index, count);
        }

        public override string ReadLine()
        {
            return base.ReadLine();
        }

        public override async Task<string> ReadLineAsync()
        {
            var buffer = new byte[1];
            var lineBuffer = new StringBuilder();
            bool encounteredCr = false;
            bool encounteredLf = false;
            while (0 != await this.BaseStream.ReadAsync(buffer, 0, buffer.Length))
            {
                var c = (char) buffer[0];
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
            return base.ReadToEnd();
        }

        public override Task<string> ReadToEndAsync()
        {
            return base.ReadToEndAsync();
        }
    }
}
