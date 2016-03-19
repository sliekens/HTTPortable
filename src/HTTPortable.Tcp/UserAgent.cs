using System.IO;
using HTTPortable.Core;

namespace HTTPortable.Tcp
{
    public class UserAgent : PortableUserAgent
    {
        public UserAgent(Stream stream)
            : base(stream)
        {
        }

        public UserAgent(Stream inputStream, Stream outputStream)
            : base(inputStream, outputStream)
        {
        }
    }
}