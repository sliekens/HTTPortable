using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPortable.Core
{
    /// <summary>Represents the method that is called after a request's headers have been written.</summary>
    /// <param name="requestMessage">The object that describes the request and its headers.</param>
    /// <param name="requestStream">The <see cref="T:System.IO.Stream" /> to write a message body to.</param>
    /// <param name="cancellationToken">
    /// The <see cref="T:System.Threading.CancellationToken" /> that provides cancellation
    /// support.
    /// </param>
    /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous write operation.</returns>
    public delegate Task OnRequestHeadersComplete(
        IRequestMessage requestMessage, Stream requestStream, CancellationToken cancellationToken);
}