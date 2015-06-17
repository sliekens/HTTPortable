namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;

    public class StatusLine : Sequence<HttpVersion, Space, StatusCode, Space, ReasonPhrase, EndOfLine>
    {
        public StatusLine(HttpVersion element1, Space element2, StatusCode element3, Space element4, ReasonPhrase element5, EndOfLine element6, ITextContext context)
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