namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;

    public class RequestLine : Sequence<Method, Space, RequestTarget, Space, HttpVersion, EndOfLine>
    {
        public RequestLine(Method element1, Space element2, RequestTarget element3, Space element4, HttpVersion element5, EndOfLine element6, ITextContext context)
            : base(element1, element2, element3, element4, element5, element6, context)
        {
            Contract.Requires(element1 != null);
            Contract.Requires(element2 != null);
            Contract.Requires(element3 != null);
            Contract.Requires(element4 != null);
            Contract.Requires(element5 != null);
            Contract.Requires(element6 != null);
            Contract.Requires(context != null);
        }
    }
}