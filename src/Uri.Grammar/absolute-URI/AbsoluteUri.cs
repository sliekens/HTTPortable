namespace Uri.Grammar
{
    using System.Diagnostics;

    using TextFx.ABNF;

    public class AbsoluteUri : Concatenation
    {
        public AbsoluteUri(Concatenation concatenation) 
            : base(concatenation)
        {
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