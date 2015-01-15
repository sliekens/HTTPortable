namespace Http.Headers
{
    using System.Globalization;

    public class ContentLengthHeader : Header
    {
        public ContentLengthHeader()
            : base("Content-Length")
        {
        }

        public ContentLengthHeader(long contentLength)
            : base("Content-Length")
        {
            this.Add(contentLength.ToString(NumberFormatInfo.InvariantInfo));
        }
    }
}