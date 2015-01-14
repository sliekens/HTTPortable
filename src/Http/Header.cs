using System.Collections.Generic;
using System.Diagnostics;

namespace Http
{
    using System.Diagnostics.Contracts;

    [DebuggerDisplay("Count = {Count}", Name = "{Name}")]
    [DebuggerTypeProxy(typeof(HeaderDebugView))]
    public class Header : List<string>, IHeader
    {
        private readonly bool optional;
        private readonly string name;

        public Header(string name)
            : this(name, optional: true)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));
        }

        public Header(string name, bool optional)
            : base(1)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));
            this.name = name;
            this.optional = optional;
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
        public bool Optional
        {
            get
            {
                return this.optional;
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
                    return header.ToArray();
                }
            }
        }
    }
}
