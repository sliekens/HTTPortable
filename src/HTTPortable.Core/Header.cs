namespace Http
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>Represents a request or response header.</summary>
    [DebuggerDisplay("Count = {Count}", Name = "{Name}")]
    [DebuggerTypeProxy(typeof(HeaderDebugView))]
    public class Header : List<string>, IHeader
    {
        /// <summary>Initializes a new instance of the <see cref="T:Http.Header" /> class with a specified header name.</summary>
        /// <param name="name">The name of the header.</param>
        public Header(string name)
            : this(name, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Http.Header" /> class with a specified header name and an
        /// additional value that indicates whether the header is required.
        /// </summary>
        /// <param name="name">The name of the header.</param>
        /// <param name="required">A value that indicates whether the header is required.</param>
        public Header(string name, bool required)
            : base(1)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Argument is null or empty", nameof(name));
            }
            Name = name;
            Required = required;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public bool Required { get; }

        [DebuggerDisplay("{Values}")]
        private class HeaderDebugView
        {
            private readonly Header header;

            public HeaderDebugView(Header header)
            {
                this.header = header;
            }

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public string[] Values => header.ToArray();
        }
    }
}