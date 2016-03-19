using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPortable.Core
{
    /// <summary>Represents the method that is called after a response's headers have been read.</summary>
    /// <param name="responseMessage">The object that describes the response and its headers.</param>
    /// <param name="responseStream">The <see cref="T:System.IO.Stream" /> to read a message body from.</param>
    /// <param name="cancellationToken">
    /// The <see cref="T:System.Threading.CancellationToken" /> that provides cancellation
    /// support.
    /// </param>
    /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous read operation.</returns>
    public delegate Task OnResponseHeadersComplete(
        IResponseMessage responseMessage, Stream responseStream, CancellationToken cancellationToken);
}