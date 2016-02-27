namespace Uri.Grammar
{
    using System.Diagnostics;

    using TextFx.ABNF;

    /// <summary>Represents a Uniform Resource Identifier (URI) as described in RFC 3986.</summary>
    /// <remarks>See: <a href="https://www.ietf.org/rfc/rfc3986.txt">RFC 3986</a>.</remarks>
    public class UniformResourceIdentifier : Concatenation
    {
        public UniformResourceIdentifier(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public Fragment Fragment
        {
            get
            {
                var optionalFragmentPart = (Repetition)this.Elements[4];
                if (optionalFragmentPart.Elements.Count == 0)
                {
                    return null;
                }

                var fragmentPart = (Concatenation)optionalFragmentPart.Elements[0];
                return (Fragment)fragmentPart.Elements[1];
            }
        }

        public HierarchicalPart HierarchicalPart
        {
            get
            {
                Debug.Assert(this.Elements[2] is HierarchicalPart, "this.Elements[2] is HierarchicalPart");
                return (HierarchicalPart)this.Elements[2];
            }
        }

        public Query Query
        {
            get
            {
                var optionalQueryPart = (Repetition)this.Elements[3];
                if (optionalQueryPart.Elements.Count == 0)
                {
                    return null;
                }

                var queryPart = (Concatenation)optionalQueryPart.Elements[0];
                return (Query)queryPart.Elements[1];
            }
        }

        public Scheme Scheme
        {
            get
            {
                Debug.Assert(this.Elements[0] is Scheme, "this.Elements[0] is Scheme");
                return (Scheme)this.Elements[0];
            }
        }
    }
}