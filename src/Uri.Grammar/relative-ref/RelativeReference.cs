namespace Uri.Grammar
{
    using System.Diagnostics;

    using TextFx.ABNF;

    public class RelativeReference : Sequence
    {
        public RelativeReference(Sequence sequence)
            : base(sequence)
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

                var fragmentPart = (Sequence)optionalFragmentPart.Elements[0];
                return (Fragment)fragmentPart.Elements[1];
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

                var queryPart = (Sequence)optionalQueryPart.Elements[0];
                return (Query)queryPart.Elements[1];
            }
        }

        public RelativePart RelativePart
        {
            get
            {
                Debug.Assert(this.Elements[0] is RelativePart, "this.Elements[0] is RelativePart");
                return (RelativePart)this.Elements[0];
            }
        }
    }
}