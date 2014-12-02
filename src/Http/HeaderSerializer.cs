using System.Linq;
using System.Text;

namespace Http
{
    public class HeaderSerializer
    {
        public string Serialize(IHeaderCollection headers)
        {
            return string.Join("\r\n", headers.Select(this.Serialize));
        }

        public string Serialize(IHeader header)
        {
            var builder = new StringBuilder();
            builder.Append(header.Name);
            builder.Append(": ");
            builder.Append(string.Join(", ", header));
            return builder.ToString();
        }
    }
}
