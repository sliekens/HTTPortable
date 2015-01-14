using System.Collections.Generic;
using System.Diagnostics;

namespace Http
{
    [DebuggerDisplay("Count = {Count}", Name = "{Name}")]
    [DebuggerTypeProxy(typeof(HeaderDebugView))]
    public class Header : List<string>, IHeader
    {
        private readonly bool optional;

        public Header(string name)
            : this(name, optional: true)
        {
        }

        public Header(string name, bool optional)
            : base(1)
        {
            this.Name = name;
            this.optional = optional;
        }

        public string Name { get; set; }

        /// <inheritdoc />
        public bool Optional
        {
            get
            {
                return this.optional;
            }
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
