namespace Http
{
    public class EntityHeaderCollection : HeaderCollection, IEntityHeaderCollection
    {
        public EntityHeaderCollection()
        {
            this.Add(this.Allow = new Header("Allow"));
            this.Add(this.ContentEncoding = new Header("Content-Encoding"));
            this.Add(this.ContentLanguage = new Header("Content-Language"));
            this.Add(this.ContentLength = new Header("Content-Length"));
            this.Add(this.ContentLocation = new Header("Content-Location"));
            this.Add(this.ContentMd5 = new Header("Content-MD5"));
            this.Add(this.ContentRange = new Header("Content-Range"));
            this.Add(this.ContentType = new Header("Content-Type"));
            this.Add(this.Expires = new Header("Expires"));
            this.Add(this.LastModified = new Header("Last-Modified"));
        }

        public IHeader Allow { get; set; }

        public IHeader ContentEncoding { get; set; }

        public IHeader ContentLanguage { get; set; }

        public IHeader ContentLength { get; set; }

        public IHeader ContentLocation { get; set; }

        public IHeader ContentMd5 { get; set; }

        public IHeader ContentRange { get; set; }

        public IHeader ContentType { get; set; }

        public IHeader Expires { get; set; }

        public IHeader LastModified { get; set; }
    }
}
