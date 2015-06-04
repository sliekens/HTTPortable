namespace Uri.Grammar
{
    using System.IO;
    using System.Text;

    internal static class StringExtensions
    {
        internal static Stream ToMemoryStream(this string instance)
        {
            return new MemoryStream(Encoding.GetEncoding("iso-8859-1").GetBytes(instance));
        }
    }
}
