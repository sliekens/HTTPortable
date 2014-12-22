namespace Http
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public delegate Task OnRequestHeadersComplete(
        IRequestMessage requestMessage, Stream requestStream, CancellationToken cancellationToken);
}