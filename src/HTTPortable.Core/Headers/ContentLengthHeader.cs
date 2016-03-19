namespace Http.Headers
{
    using System.Globalization;

    public class ContentLengthHeader : Header
    {
        public const string FieldName = "Content-Length";

        public ContentLengthHeader()
            : base(FieldName)
        {
        }

        public ContentLengthHeader(long contentLength)
            : base(FieldName)
        {
            Add(contentLength.ToString(NumberFormatInfo.InvariantInfo));
        }
    }
}