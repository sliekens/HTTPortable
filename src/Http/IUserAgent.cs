using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Http
{
    public interface IUserAgent
    {
        Task SendAsync(IRequestMessage message, CancellationToken cancellationToken, Func<Stream, CancellationToken, Task> writeAsync = null);
    }
}
