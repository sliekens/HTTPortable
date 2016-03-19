namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class HierarchicalPart : Alternative
    {
        public HierarchicalPart(Alternative alternative)
            : base(alternative)
        {
        }

        public Authority Authority
        {
            get
            {
                if (Ordinal != 1)
                {
                    return null;
                }

                var concatenation = (Concatenation)Element;
                return (Authority)concatenation.Elements[1];
            }
        }

        public PathAbsoluteOrEmpty PathAbsoluteOrEmpty
        {
            get
            {
                if (Ordinal != 1)
                {
                    return null;
                }

                var concatenation = (Concatenation)Element;
                return (PathAbsoluteOrEmpty)concatenation.Elements[2];
            }
        }

        public PathAbsolute PathAbsolute
        {
            get
            {
                if (Ordinal != 2)
                {
                    return null;
                }

                return (PathAbsolute)Element;
            }
        }

        public PathRootless PathRootless
        {
            get
            {
                if (Ordinal != 3)
                {
                    return null;
                }

                return (PathRootless)Element;
            }
        }

        public PathEmpty PathEmpty
        {
            get
            {
                if (Ordinal != 4)
                {
                    return null;
                }

                return (PathEmpty)Element;
            }
        }
    }
}