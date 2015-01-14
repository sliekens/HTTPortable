using System.Collections.Generic;
using System.Diagnostics;

namespace Http
{
    using System.Diagnostics.Contracts;

    [DebuggerDisplay("Count = {Count}", Name = "{Name}")]
    [DebuggerTypeProxy(typeof(HeaderDebugView))]
    public class Header : List<string>, IHeader
    {
        private readonly bool required;
        private readonly string name;

        public Header(string name)
            : this(name, required: false)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));
        }

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
                    return header.ToArray();
                }
            }
        }
    }
}
