using System.Collections.Generic;

namespace Http
{
    public class Header : List<string>, IHeader
    {
        public Header(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
