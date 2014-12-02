namespace Http
{
    public interface IResponseHeaderCollection : IHeaderCollection
    {
        IHeader AcceptRanges { get; set; }
        IHeader Age { get; set; }
        IHeader ETag { get; set; }
        IHeader Location { get; set; }
        IHeader ProxyAuthenticate { get; set; }
        IHeader RetryAfter { get; set; }
        IHeader Server { get; set; }
        IHeader Vary { get; set; }
        IHeader WwwAuthenticate { get; set; }
    }
}