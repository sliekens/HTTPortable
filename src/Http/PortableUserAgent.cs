using System;
using System.IO;
using System.Linq;
using System.Threading;
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

        public async Task SendAsync(IRequestMessage message, CancellationToken cancellationToken, Func<Stream, CancellationToken, Task> writeAsync = null)
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
                    await writeAsync(stream, cancellationToken).ConfigureAwait(false);
                }

                await writer.FlushAsync().ConfigureAwait(false);
            }

            // TODO: what should this method actually return?
            // How about returning a subscription object that publishes the response when it becomes available?
            // Requests to the same host can be pipelined as long as the responses are returned in the same order.
            // When requests are pipelined, it is impossible to determine ahead of time when the request will actually be sent, and also if or when the response will come back, because the sender does not get to inspect the current traffic situation on the pipeline.
            // We can decouple requests and responses as long as we keep count of requests sent and responses received, so that we can bring requests/responses back together by the order in which they were received.
            // This pipelining stuff raises other questions
            // - How is pipelining implemented anyway?
            //     - For all requests to the same host (e.g. 1 pipeline per host, unlimited requests per pipeline)?
            //     - For limited batches of requests to the same host (e.g. multiple pipelines, max 10 requests per pipeline)?
            // - What if a single response takes a long time to complete? Does it block requests that were queued later on? I think that sending requests to the server is always fast, but the response for subsequent requests won't come back until after the slow response has completed.
            // -- Imagine pipelining a request for 2GB of binary data, and then another request for 2MB of text data. The text file, even though it's much lighter to process, will be stuck in the response pipeline until after the binary data has been received.
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
            foreach (var header in headerCollection.Where(headerHasValue).OrderBy(h => h.Name, StringComparer.Ordinal))
            {
                await writer.WriteAsync(header.Name).ConfigureAwait(false);
                await writer.WriteAsync(": ").ConfigureAwait(false);
                await writer.WriteAsync(header.First()).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);
            }
        }
    }
}