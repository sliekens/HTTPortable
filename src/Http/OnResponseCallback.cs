namespace Http
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public delegate Task OnResponseHeadersComplete(IResponseMessage responseMessage, Stream responseStream, CancellationToken cancellationToken);
}