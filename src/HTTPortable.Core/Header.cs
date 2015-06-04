namespace Http
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    /// <summary>Represents a request or response header.</summary>
    [DebuggerDisplay("Count = {Count}", Name = "{Name}")]
    [DebuggerTypeProxy(typeof(HeaderDebugView))]
    public class Header : List<string>, IHeader
    {
        private readonly string name;
        private readonly bool required;

        /// <summary>Initializes a new instance of the <see cref="T:Http.Header" /> class with a specified header name.</summary>
        /// <param name="name">The name of the header.</param>
        public Header(string name)
            : this(name, false)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));
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
            Contract.Requires(!string.IsNullOrEmpty(name));
            this.name = name;
            this.required = required;
        }

        /// <inheritdoc />
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <inheritdoc />
        public bool Required
        {
            get
            {
                return this.required;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(!string.IsNullOrEmpty(this.name));
        }

        [DebuggerDisplay("{Values}")]
        private class HeaderDebugView
        {
            private readonly Header header;

            public HeaderDebugView(Header header)
            {
                this.header = header;
            }

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public string[] Values
            {
                get
                {
                    return this.header.ToArray();
                }
            }
        }
    }
}