using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Http.header_field;
using Http.Headers;
using Http.HTTP_message;
using Http.method;
using Http.request_line;
using Http.start_line;
using Http.status_line;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.CR;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.LF;

namespace Http
{
    public class UserAgent : IUserAgent
    {
        private readonly Stream inputStream;
        private readonly Stream outputStream;

        private readonly ILexer<HttpMessage> httpMessageLexer;

        /// <summary>Indicates whether this object has been disposed.</summary>
        private bool disposed;

        public UserAgent(Stream stream, ILexer<HttpMessage> httpMessageLexer)
            : this(stream, stream, httpMessageLexer)
        {
            this.httpMessageLexer = httpMessageLexer;
        }

        public UserAgent(Stream inputStream, Stream outputStream, [NotNull] ILexer<HttpMessage> httpMessageLexer)
        {
            if (httpMessageLexer == null)
            {
                throw new ArgumentNullException(nameof(httpMessageLexer));
            }
            this.inputStream = inputStream;
            this.outputStream = outputStream;
            this.httpMessageLexer = httpMessageLexer;
        }

        /// <summary>This method calls <see cref="Dispose(bool)" />, specifying <c>true</c> to release all resources.</summary>
        public void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task ReceiveAsync(CancellationToken cancellationToken, OnResponseHeadersComplete callback = null)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            {
                using (ITextScanner scanner = new TextScanner(new StreamTextSource(pushbackInputStream, Encoding.UTF8)))
                {
                    var result = httpMessageLexer.Read(scanner);
                    throw new NotImplementedException();
                }
            }
        }

        public async Task SendAsync(IRequestMessage message, CancellationToken cancellationToken,
            OnRequestHeadersComplete callback = null)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            using (var writer = new StreamWriter(outputStream, new UTF8Encoding(false), 512, true))
            {
                await WriteRequestLineAsync(writer, message).ConfigureAwait(false);
                await WriteHeadersAsync(writer, message.Headers).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);
                await writer.FlushAsync().ConfigureAwait(false);
                if (callback != null)
                {
                    long contentLength;
                    if (!message.Headers.TryGetContentLength(out contentLength))
                    {
                        contentLength = 0;
                    }

                    using (var messageBodyStream = new MessageBodyStream(writer.BaseStream, contentLength))
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
            // - For all requests to the same host (e.g. 1 pipeline per host, unlimited requests per pipeline)?
            // - For limited batches of requests to the same host (e.g. multiple pipelines, max 10 requests per pipeline)?
            // - What if a single response takes a long time to complete? Does it block requests that were queued later on? I think that sending requests to the server is always fast, but the response for subsequent requests won't come back until after the slow response has completed.
            // -- Imagine pipelining a request for 2GB of binary data, and then another request for 2MB of text data. The text file, even though it's much lighter to process, will be stuck in the response pipeline until after the binary data has been received.
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        void IDisposable.Dispose()
        {
            Close();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <param name="disposing">
        /// <c>true</c> to clean up both managed and unmanaged resources; otherwise, <c>false</c> to clean up only unmanaged
        /// resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                inputStream.Dispose();
                outputStream.Dispose();
            }

            disposed = true;
        }

        /// <summary>
        /// Gets a value indicating whether the given header should be included in a request.
        /// By default, headers should be included if they have one or more values.
        /// Additionally, an implementation of the <see cref="IHeader"/> interface is allowed to override
        /// the <see cref="IHeader.Required"/> property in a way that indicates that the header must be included in a request,
        /// even if it has no values.
        /// </summary>
        /// <param name="header">The header to evaluate.</param>
        /// <returns><c>true</c> if the header should be included in a request; otherwise, <c>false</c>.</returns>
        private static bool ShouldSendHeader(IHeader header)
        {
            return header.Required || header.Any();
        }

        private static async Task WriteHeadersAsync(StreamWriter writer, IHeaderCollection headerCollection)
        {
            foreach (var header in headerCollection.Where(ShouldSendHeader).OrderBy(h => h.Name, StringComparer.Ordinal)
                )
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