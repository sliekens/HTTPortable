using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Http
{
    public class UserAgent : IUserAgent
    {
        public async Task<IResponseMessage> SendAsync(IRequestMessage message, Func<Stream, Task> writeAsync = null)
        {
            var request = WebRequest.CreateHttp(message.RequestUri);
            request.Method = message.Method;
            foreach (var header in message.RequestHeaders.Where(header => header.Any()))
            {
                if (header.Name.Equals("Accept", StringComparison.Ordinal))
                {
                    request.Accept = header.FirstOrDefault();
                }
                else if (header.Name.Equals("Expect", StringComparison.Ordinal))
                {
                    // Ignore this header for this implementation — the .NET framework
                    // automatically injects 'Expect: 100-continue' into the request.
                    // This behavior cannot be modified or disabled in portable class libraries.
                }
                else if (header.Name.Equals("Host", StringComparison.Ordinal))
                {
                    // Ignore this header for this implementation — the .NET framework
                    // extracts host information from the request URI.
                    // This behavior cannot be modified or disabled.
                }
                else if (header.Name.Equals("If-Modified-Since", StringComparison.Ordinal))
                {
                    // Ignore this header for this implementation — the .NET framework
                    // does not support this header in portable class libraries.
                }
                else if (header.Name.Equals("Range", StringComparison.Ordinal))
                {
                    // Ignore this header for this implementation — the .NET framework
                    // does not support this header in portable class libraries.
                }
                else if (header.Name.Equals("Referer", StringComparison.Ordinal))
                {
                    // Ignore this header for this implementation — the .NET framework
                    // does not support this header in portable class libraries.
                }
                else if (header.Name.Equals("User-Agent", StringComparison.Ordinal))
                {
                    // Ignore this header for this implementation — the .NET framework
                    // does not support this header in portable class libraries.
                }
                else
                {
                    request.Headers[header.Name] = header.FirstOrDefault();
                }
            }

            if (writeAsync != null)
            {
                request.ContentType = message.ContentHeaders.ContentType.FirstOrDefault();
                var stream = await request.GetRequestStreamAsync().ConfigureAwait(false);
                using (stream)
                {
                    await writeAsync(stream).ConfigureAwait(false);
                }
            }

            var response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false);

            var responseMessage = new ResponseMessage
            {
                Status = response.StatusCode,
                Reason = response.StatusDescription
            };

            var generalHeaders = responseMessage.Headers;
            var responseHeaders = responseMessage.ResponseHeaders;
            var contentHeaders = responseMessage.ContentHeaders;
            foreach (var headerName in response.Headers.AllKeys)
            {
                var name = headerName;
                var predicate = new Func<IHeader, bool>(h => h.Name.Equals(name, StringComparison.Ordinal));
                var header = generalHeaders.FirstOrDefault(predicate) ?? responseHeaders.FirstOrDefault(predicate) ?? contentHeaders.FirstOrDefault(predicate);
                if (header == null)
                {
                    header = new Header(headerName);
                    contentHeaders.Add(header);
                }

                header.Add(response.Headers[headerName]);
            }

            return responseMessage;
        }
    }
}
