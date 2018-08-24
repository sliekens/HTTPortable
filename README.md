#HTTPortable
[![Build status](https://ci.appveyor.com/api/projects/status/76485w5o6npwh6bh?svg=true)](https://ci.appveyor.com/project/StevenLiekens/httportable)

# About
HTTPortable provides a cross-platform, .NET based implementation of the HTTP protocol.

# ON HOLD
Development of HTTPortable is currently on hold while I work out some problems with [Txt](https://github.com/StevenLiekens/Txt).

# Background and Goals

HTTP is typically spoken over TCP, but it is fundamentally transport agnostic.

Taken from RFC 7230:
> HTTP messaging is independent of the underlying transport- or
> session-layer connection protocol(s).  HTTP only presumes a reliable
> transport with in-order delivery of requests and the corresponding
> in-order delivery of responses.

Unfortunately, most HTTP libraries are hard-coded to use TCP/IP sockets. That includes the .NET framework's `System.Net.WebRequest` API and also the ASP.NET framework in its entirety.

The key goal for this project is to allow any `System.IO.Stream` to be used as the transport channel. This opens up tons of new possibilities. For example: HTTP over RFCOMM.

The secondary goal for this project is to work the same across all supported platforms. It is my opinion that you should only have to test managed code on one platform to be certain that it will work everywhere. That can be accomplished by only using managed code. This is different from other HTTP APIs in .NET that call into native APIs.
For example: the HttpClient class in .NET Core uses WinHTTP on Windows and libcurl on Unix variants. Those two implementations behave differently, making it more expensive to write code for multiple platforms.

# Specifications
Code is based on the latest RFCs regarding version HTTP/1.1.  

* RFC 7230: [Message Syntax and Routing](http://tools.ietf.org/html/rfc7230)
* RFC 7231: [Semantics and Content](http://tools.ietf.org/html/rfc7231)
* RFC 7232: [Conditional Requests](http://tools.ietf.org/html/rfc7232)
* RFC 7233: [Range Request](http://tools.ietf.org/html/rfc7233)
* RFC 7234: [Caching](http://tools.ietf.org/html/rfc7234)
* RFC 7235: [Authentication](http://tools.ietf.org/html/rfc7235)
* RFC 7236: [Authentication Scheme Registrations](http://tools.ietf.org/html/rfc7236)
* RFC 7237: [Method Registrations](http://tools.ietf.org/html/rfc7237)
* RFC 7238: [the 308 status code](http://tools.ietf.org/html/rfc7238)
* RFC 7239: [Forwarded HTTP extension](http://tools.ietf.org/html/rfc7239)

# Example

This code example demonstrates how to use the library to download a zip file that contains the most recent version of the code, from the GitHub back-end servers, over HTTP.

## Points of interest:

* This example is specific to desktop applications. Portable applications use a different set of classes to set up secure TCP connections.
* The zip file is saved to the "Downloads" folder in the current user's profile directory.
* GitHub includes a `Content-Disposition` header in the response that specifies a file name for the zip file.

## Code

```c#
using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Http;
using Http.Tcp;
```

```c#
class Program
{
    static void Main(string[] args)
    {
        new Program().Download().Wait();
    }

    public async Task Download()
    {
        // Create a new HTTP/1.1 request
        var request = new RequestMessage("GET", "/StevenLiekens/http-client/zip/master", Version.Parse("1.1"));

        // Set the 'Host' header
        request.Headers.Add(new Header("Host") { "codeload.github.com" });

        // Set the 'User-Agent' header
        request.Headers.Add(new Header("User-Agent") { "https://github.com/StevenLiekens/http-client" });

        // Connect to GitHub over TCP/IP
        var tcpClient = new TcpClient();
        await tcpClient.ConnectAsync("codeload.github.com", 443);

        // Switch to SSL
        var sslStream = new SslStream(tcpClient.GetStream());
        await sslStream.AuthenticateAsClientAsync("codeload.github.com");

        // Create a new user agent object for the given inbound and outbound streams (in/out are the same in this case)
        var userAgent = new UserAgent(sslStream);

        // Asynchronously send the request
        await userAgent.SendAsync(request, CancellationToken.None);

        // Asynchronously execute the given callback method for the response
        await userAgent.ReceiveAsync(CancellationToken.None, (message, stream, cancellationToken) =>
        {
            return Task.Run(() =>
            {
                // Get the file name from the response headers
                var contentDispositionHeader = message.Headers.First(h => h.Name.Equals("Content-Disposition")).First();
                var contentDisposition = new ContentDisposition(contentDispositionHeader);
                var downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", contentDisposition.FileName);

                // Save the message body to disk
                using (var fileStream = File.OpenWrite(downloadPath))
                {
                    stream.CopyTo(fileStream);
                }
            }, cancellationToken);
        });

    }
}
```
