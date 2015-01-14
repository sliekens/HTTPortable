namespace Http
{
    using System.Collections.Generic;

    /// <summary>Provides the interface for types that represent a request or response header.</summary>
    public interface IHeader : IList<string>
    {
        /// <summary>Gets the name of the header. This is the value that is used to identify headers in a message.</summary>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether the header is optional. An optional header is a header that may be ommitted if
        /// there are no values associated with that header. A required header will always be included in the message, even if it
        /// has no value associated with it.
        /// </summary>
        bool Optional { get; }
    }
}