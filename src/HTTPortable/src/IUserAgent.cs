namespace Http
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUserAgent : IDisposable
    {
        Task ReceiveAsync(CancellationToken cancellationToken, OnResponseHeadersComplete callback = null);

        Task SendAsync(IRequestMessage message, CancellationToken cancellationToken, 
            OnRequestHeadersComplete callback = null);
    }
}