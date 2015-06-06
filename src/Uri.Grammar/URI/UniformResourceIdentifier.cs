namespace Uri.Grammar
{
    using SLANG;

    /// <summary>Represents a Uniform Resource Identifier (URI) as described in RFC 3986.</summary>
    /// <remarks>See: <a href="https://www.ietf.org/rfc/rfc3986.txt">RFC 3986</a>.</remarks>
    public class UniformResourceIdentifier : Sequence
    {
        public UniformResourceIdentifier(Sequence sequence)
            : base(sequence)
        {
        }
    }
}