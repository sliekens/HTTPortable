namespace Http
{
    public class GeneralHeaderCollection : HeaderCollection, IGeneralHeaderCollection
    {
        public GeneralHeaderCollection()
        {
            this.Add(this.Connection = new Header("Connection"));
            this.Add(this.CacheControl = new Header("Cache-Control"));
            this.Add(this.Date = new Header("Date"));
            this.Add(this.Pragma = new Header("Pragma"));
            this.Add(this.Trailer = new Header("Trailer"));
            this.Add(this.TransferEncoding = new Header("Transfer-Encoding"));
            this.Add(this.Upgrade = new Header("Upgrade"));
            this.Add(this.Via = new Header("Via"));
            this.Add(this.Warning = new Header("Warning"));
        }

        public IHeader CacheControl { get; set; }
        
        public IHeader Connection { get; set; }

        public IHeader Date { get; set; }

        public IHeader Pragma { get; set; }

        public IHeader Trailer { get; set; }

        public IHeader TransferEncoding { get; set; }

        public IHeader Upgrade { get; set; }

        public IHeader Via { get; set; }

        public IHeader Warning { get; set; }
    }
}