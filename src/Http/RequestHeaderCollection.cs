namespace Http
{
    public class RequestHeaderCollection : HeaderCollection, IRequestHeaderCollection
    {
        public RequestHeaderCollection()
        {
            this.Add(this.Accept = new Header("Accept"));
            this.Add(this.AcceptCharset = new Header("Accept-Charset"));
            this.Add(this.AcceptEncoding = new Header("Accept-Encoding"));
            this.Add(this.AcceptLanguage = new Header("Accept-Language"));
            this.Add(this.Authorization = new Header("Authorization"));
            this.Add(this.Expect = new Header("Expect"));
            this.Add(this.From = new Header("From"));
            this.Add(this.Host = new Header("Host"));
            this.Add(this.IfMatch = new Header("If-Match"));
            this.Add(this.IfModifiedSince = new Header("If-Modified-Since"));
            this.Add(this.IfNoneMatch = new Header("If-None-Match"));
            this.Add(this.IfRange = new Header("If-Range"));
            this.Add(this.IfUnmodifiedSince = new Header("If-Unmodified-Since"));
            this.Add(this.MaxForwards = new Header("Max-Forwards"));
            this.Add(this.ProxyAuthorization = new Header("Proxy-Authorization"));
            this.Add(this.Range = new Header("Range"));
            this.Add(this.Referer = new Header("Referer"));
            this.Add(this.TE = new Header("TE"));
            this.Add(this.UserAgent = new Header("User-Agent"));
        }

        public IHeader Accept { get; set; }

        public IHeader AcceptCharset { get; set; }

        public IHeader AcceptEncoding { get; set; }

        public IHeader AcceptLanguage { get; set; }

        public IHeader Authorization { get; set; }

        public IHeader Expect { get; set; }

        public IHeader From { get; set; }

        public IHeader Host { get; set; }

        public IHeader IfMatch { get; set; }

        public IHeader IfModifiedSince { get; set; }

        public IHeader IfNoneMatch { get; set; }

        public IHeader IfRange { get; set; }

        public IHeader IfUnmodifiedSince { get; set; }

        public IHeader MaxForwards { get; set; }

        public IHeader ProxyAuthorization { get; set; }

        public IHeader Range { get; set; }

        public IHeader Referer { get; set; }

        public IHeader TE { get; set; }

        public IHeader UserAgent { get; set; }

        protected override string GetKeyForItem(IHeader item)
        {
            return item.Name;
        }
    }
}