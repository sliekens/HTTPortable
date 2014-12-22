namespace Http
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Grammars.Rfc7230;
    using Text.Scanning;

    public class PortableUserAgent : IUserAgent
    {
        /// <summary>Indicates whether this object has been disposed.</summary>
        private bool disposed;

        private readonly Stream inputStream;
        private readonly Stream outputStream;

        public PortableUserAgent(Stream inputStream, Stream outputStream)
        {
            this.inputStream = inputStream;
            this.outputStream = outputStream;
        }

        /// <summary>This method calls <see cref="Dispose(bool)" />, specifying <c>true</c> to release all resources.</summary>
        public void Close()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task ReceiveAsync(CancellationToken cancellationToken, OnResponseHeadersComplete callback = null)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            var message = new ResponseMessage();
            using (var reader = new LazyStreamReader(this.inputStream, Encoding.UTF8, false, 1, true))
            {
                using (ITextScanner scanner = new TextScanner(reader))
                {
                    scanner.Read();
                    var lexer = new StatusLineLexer();
                    var statusLine = lexer.Read(scanner);
                    message.Version = statusLine.HttpVersion.ToVersion();
                    message.Status = int.Parse(statusLine.StatusCode.Data);
                    message.Reason = statusLine.ReasonPhrase.Data;
                }

                string line;
                while ((line = await reader.ReadLineAsync()) != string.Empty)
                {
                    var rawHeader = line.Split(new[] { ':', ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    var name = rawHeader[0];
                    var value = rawHeader[1];
                    var predicate = new Func<IHeader, bool>(h => h.Name.Equals(name, StringComparison.Ordinal));
                    var header = message.Headers.SingleOrDefault(predicate);
                    if (header == null)
                    {
                        message.Headers.Add(header = new Header(name));
                    }

                    header.Add(value);
                }

                using (var messageBodyStream = new MessageBodyStream(message, reader.BaseStream))
                {
                    // Invoke the callback (if specified) that will optionally consume the message body
                    if (callback != null)
                    {
                        await callback(message, messageBodyStream, cancellationToken);
                    }

                    // Swallow remaining bytes (if any)
                    if (messageBodyStream.Position < messageBodyStream.Length)
                    {
                        await messageBodyStream.CopyToAsync(Stream.Null);
                    }
                }
            }
        }

        public async Task SendAsync(IRequestMessage message, CancellationToken cancellationToken,
            OnRequestHeadersComplete callback = null)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            using (var writer = new StreamWriter(this.outputStream, new UTF8Encoding(false), 512, true))
            {
                await WriteRequestLineAsync(writer, message).ConfigureAwait(false);
                await WriteHeadersAsync(writer, message.Headers).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);
                await writer.FlushAsync().ConfigureAwait(false);
                if (callback != null)
                {
                    using (var messageBodyStream = new MessageBodyStream(message, writer.BaseStream))
                    {
                        await callback(message, messageBodyStream, cancellationToken).ConfigureAwait(false);
                    }
                }
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

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        void IDisposable.Dispose()
        {
            this.Close();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <param name="disposing">
        /// <c>true</c> to clean up both managed and unmanaged resources; otherwise, <c>false</c> to clean up only unmanaged
        /// resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.inputStream.Dispose();
                this.outputStream.Dispose();
            }

            this.disposed = true;
        }

        private static async Task WriteHeadersAsync(StreamWriter writer, IHeaderCollection headerCollection)
        {
            var headerHasValue = new Func<IHeader, bool>(header => !header.Optional || header.Any());
            foreach (var header in headerCollection.Where(headerHasValue).OrderBy(h => h.Name, StringComparer.Ordinal))
            {
                await writer.WriteAsync(header.Name).ConfigureAwait(false);
                await writer.WriteAsync(": ").ConfigureAwait(false);
                await writer.WriteAsync(header.First()).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);
            }
        }

        private static async Task WriteRequestLineAsync(StreamWriter writer, IRequestMessage message)
        {
            await writer.WriteAsync(message.Method).ConfigureAwait(false);
            await writer.WriteAsync(' ').ConfigureAwait(false);
            await writer.WriteAsync(message.RequestUri).ConfigureAwait(false);
            await writer.WriteAsync(' ').ConfigureAwait(false);
            await writer.WriteAsync("HTTP/").ConfigureAwait(false);
            await writer.WriteAsync(message.HttpVersion.ToString(2)).ConfigureAwait(false);
            await writer.WriteLineAsync().ConfigureAwait(false);
        }
    }
}