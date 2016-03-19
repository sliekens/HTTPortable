using System;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPortable.Core
{
    public interface IUserAgent : IDisposable
    {
        Task ReceiveAsync(CancellationToken cancellationToken, OnResponseHeadersComplete callback = null);

        Task SendAsync(IRequestMessage message, CancellationToken cancellationToken, 
            OnRequestHeadersComplete callback = null);
    }
}