namespace Http
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public delegate Task OnResponseCallback(
        IResponseMessage responseMessage, Stream responseStream, CancellationToken cancellationToken);
}