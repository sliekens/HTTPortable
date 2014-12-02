namespace Http
{
    public interface IGeneralHeaderCollection : IHeaderCollection
    {
        IHeader CacheControl { get; set; }
        IHeader Connection { get; set; }
        IHeader Date { get; set; }
        IHeader Pragma { get; set; }
        IHeader Trailer { get; set; }
        IHeader TransferEncoding { get; set; }
        IHeader Upgrade { get; set; }
        IHeader Via { get; set; }
        IHeader Warning { get; set; }
    }
}