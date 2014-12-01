using System.Collections.Generic;

namespace Http.Headers
{
    public abstract class Header : Dictionary<string, IList<string>>, IHeader
    {
    }
}
