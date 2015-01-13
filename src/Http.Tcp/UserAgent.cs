namespace Http.Tcp
{
    using System.IO;

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