using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Http
{
    public interface IUserAgent : IDisposable
    {
        Task SendAsync(IRequestMessage message, CancellationToken cancellationToken, Func<Stream, CancellationToken, Task> writeAsync = null);
        Task ReceiveAsync(CancellationToken cancellationToken, Func<IResponseMessage, Stream, CancellationToken, Task> readAsync = null);
    }
}
