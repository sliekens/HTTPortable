namespace Uri.Grammar
{
    using System.Diagnostics;

    using TextFx.ABNF;

    public class RelativeReference : Concatenation
    {
        public RelativeReference(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public Fragment Fragment
        {
            get
            {
                var optionalFragmentPart = (Repetition)Elements[4];
                if (optionalFragmentPart.Elements.Count == 0)
                {
                    return null;
                }

                var fragmentPart = (Concatenation)optionalFragmentPart.Elements[0];
                return (Fragment)fragmentPart.Elements[1];
            }
        }

        public Query Query
        {
            get
            {
                var optionalQueryPart = (Repetition)Elements[3];
                if (optionalQueryPart.Elements.Count == 0)
                {
                    return null;
                }

                var queryPart = (Concatenation)optionalQueryPart.Elements[0];
                return (Query)queryPart.Elements[1];
            }
        }

        public RelativePart RelativePart
        {
            get
            {
                Debug.Assert(Elements[0] is RelativePart, "this.Elements[0] is RelativePart");
                return (RelativePart)Elements[0];
            }
        }
    }
}