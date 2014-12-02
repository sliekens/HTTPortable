namespace Http
{
    public class ResponseHeaderCollection : HeaderCollection, IResponseHeaderCollection
    {
        public ResponseHeaderCollection()
        {
            this.Add(this.AcceptRanges = new Header("Accept-Ranges"));
            this.Add(this.Age = new Header("Age"));
            this.Add(this.ETag = new Header("ETag"));
            this.Add(this.Location = new Header("Location"));
            this.Add(this.ProxyAuthenticate = new Header("Proxy-Authenticate"));
            this.Add(this.RetryAfter = new Header("Retry-After"));
            this.Add(this.Server = new Header("Server"));
            this.Add(this.Vary = new Header("Vary"));
            this.Add(this.WwwAuthenticate = new Header("WWW-Authenticate"));
        }

        public IHeader AcceptRanges { get; set; }

        public IHeader Age { get; set; }

        public IHeader ETag { get; set; }

        public IHeader Location { get; set; }

        public IHeader ProxyAuthenticate { get; set; }

        public IHeader RetryAfter { get; set; }

        public IHeader Server { get; set; }

        public IHeader Vary { get; set; }

        public IHeader WwwAuthenticate { get; set; }
    }
}