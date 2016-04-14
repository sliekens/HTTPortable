using System.Threading;
using System.Threading.Tasks;

namespace Http
{
    /// <summary>Provides the interface for factory classes that create instances of classes that implement the
    /// <see cref="IUserAgent" /> interface.</summary>
    public interface IUserAgentFactory
    {
        /// <summary>Creates and configures a user agent for the specified URI.</summary>
        /// <param name="uri">The destination URI.</param>
        /// <param name="cancellationToken">The token that provides cancellation support.</param>
        /// <returns>A user agent that connects with the specified destination.</returns>
        Task<IUserAgent> CreateAsync(System.Uri uri, CancellationToken cancellationToken);
    }
}