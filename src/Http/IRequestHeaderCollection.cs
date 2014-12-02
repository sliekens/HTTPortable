namespace Http
{
    public interface IRequestHeaderCollection : IHeaderCollection
    {
        IHeader Accept { get; set; }
        IHeader AcceptCharset { get; set; }
        IHeader AcceptEncoding { get; set; }
        IHeader AcceptLanguage { get; set; }
        IHeader Authorization { get; set; }
        IHeader Expect { get; set; }
        IHeader From { get; set; }
        IHeader Host { get; set; }
        IHeader IfMatch { get; set; }
        IHeader IfModifiedSince { get; set; }
        IHeader IfNoneMatch { get; set; }
        IHeader IfRange { get; set; }
        IHeader IfUnmodifiedSince { get; set; }
        IHeader MaxForwards { get; set; }
        IHeader ProxyAuthorization { get; set; }
        IHeader Range { get; set; }
        IHeader Referer { get; set; }
        IHeader TE { get; set; }
        IHeader UserAgent { get; set; }
    }
}