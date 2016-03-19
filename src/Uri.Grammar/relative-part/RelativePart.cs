namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class RelativePart : Alternative
    {
        public RelativePart(Alternative alternative)
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

        public PathNoScheme PathNoScheme
        {
            get
            {
                if (Ordinal != 3)
                {
                    return null;
                }

                return (PathNoScheme)Element;
            }
        }
    }
}