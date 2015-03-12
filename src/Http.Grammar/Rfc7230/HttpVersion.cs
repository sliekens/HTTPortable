namespace Http.Grammar.Rfc7230
{
    using System;
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class HttpVersion : Sequence<HttpName, Element, Digit, Element, Digit>
    {
        public HttpVersion(HttpName element1, Element element2, Digit element3, Element element4, Digit element5, ITextContext context)
            : base(element1, element2, element3, element4, element5, context)
        {
            Contract.Requires(element1 != null);
            Contract.Requires(element2 != null);
            Contract.Requires(element3 != null);
            Contract.Requires(element4 != null);
            Contract.Requires(element5 != null);
            Contract.Requires(context != null);
        }

        public Version ToVersion()
        {
            var major = int.Parse(this.Element3.Data);
            var minor = int.Parse(this.Element5.Data);
            return new Version(major, minor);
        }
    }
}