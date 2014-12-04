using System.Collections.Generic;
using System.Diagnostics;

namespace Http
{
    [DebuggerDisplay("Count = {Count}", Name = "{Name}")]
    [DebuggerTypeProxy(typeof(HeaderDebugView))]
    public class Header : List<string>, IHeader
    {
        private bool optional = true;

        public Header(string name)
            : base(1)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public bool Optional
        {
            get
            {
                return this.optional;
            }
            set
            {
                this.optional = value;
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
