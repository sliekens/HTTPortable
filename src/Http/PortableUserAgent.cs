using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Http
{
    public class PortableUserAgent : IUserAgent
    {
        private readonly Stream stream;

        public PortableUserAgent(Stream stream)
        {
            this.stream = stream;
        }

        public async Task<IResponseMessage> SendAsync(IRequestMessage message, Func<Stream, Task> writeAsync = null)
        {
            using (stream)
            using (var writer = new StreamWriter(stream))
            {
                await WriteRequestLineAsync(writer, message).ConfigureAwait(false);
                await WriteHeadersAsync(writer, message.Headers).ConfigureAwait(false);
                await WriteHeadersAsync(writer, message.RequestHeaders).ConfigureAwait(false);
                await WriteHeadersAsync(writer, message.ContentHeaders).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);
                await writer.FlushAsync().ConfigureAwait(false);

                if (writeAsync != null)
                {
                    await writeAsync(stream).ConfigureAwait(false);
                }

                await writer.FlushAsync().ConfigureAwait(false);

                stream.Seek(0, SeekOrigin.Begin);
                StreamReader reader = new StreamReader(stream);
                var txt = reader.ReadToEnd();
            }

            throw new NotImplementedException();
            return null;
        }

        private static async Task WriteRequestLineAsync(StreamWriter writer, IRequestMessage message)
        {
            await writer.WriteAsync(message.Method).ConfigureAwait(false);
            await writer.WriteAsync(' ').ConfigureAwait(false);
            await writer.WriteAsync(message.RequestUri).ConfigureAwait(false);
            await writer.WriteAsync(' ').ConfigureAwait(false);
            await writer.WriteAsync(message.HttpVersion.ToString(2)).ConfigureAwait(false);
            await writer.WriteLineAsync().ConfigureAwait(false);
        }

        private static async Task WriteHeadersAsync(StreamWriter writer, IHeaderCollection headerCollection)
        {
            var headerHasValue = new Func<IHeader, bool>(header => header.Any());
            foreach (var header in headerCollection.Where(headerHasValue))
            {
                await writer.WriteAsync(header.Name).ConfigureAwait(false);
                await writer.WriteAsync(": ").ConfigureAwait(false);
                await writer.WriteAsync(header.First()).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);
            }
        }
    }
}
