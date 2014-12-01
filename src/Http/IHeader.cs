using System.Collections.Generic;

namespace Http
{
    public interface IHeader : IDictionary<string, IList<string>>
    {
    }
}
