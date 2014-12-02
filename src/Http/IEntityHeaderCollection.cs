namespace Http
{
    public interface IEntityHeaderCollection : IHeaderCollection
    {
        IHeader Allow { get; set; }
        IHeader ContentEncoding { get; set; }
        IHeader ContentLanguage { get; set; }
        IHeader ContentLength { get; set; }
        IHeader ContentLocation { get; set; }
        IHeader ContentMd5 { get; set; }
        IHeader ContentRange { get; set; }
        IHeader ContentType { get; set; }
        IHeader Expires { get; set; }
        IHeader LastModified { get; set; }
    }
}