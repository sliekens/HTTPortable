namespace Http
{
    using System.Collections.Generic;

    /// <summary>Provides the interface for types that represent a request or response header.</summary>
    public interface IHeader : IList<string>
    {
        /// <summary>Gets the name of the header. This is the value that is used to identify headers in a message.</summary>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether the header is required. A required header will always be included in the
        /// message, even if it has no value associated with it. An optional header is a header that may be ommitted if there are
        /// no values associated with that header.
        /// </summary>
        bool Required { get; }
    }
}