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
                if (this.Ordinal != 1)
                {
                    return null;
                }

                var concatenation = (Concatenation)this.Element;
                return (Authority)concatenation.Elements[1];
            }
        }

        public PathAbsolute PathAbsolute
        {
            get
            {
                if (this.Ordinal != 2)
                {
                    return null;
                }

                return (PathAbsolute)this.Element;
            }
        }

        public PathAbsoluteOrEmpty PathAbsoluteOrEmpty
        {
            get
            {
                if (this.Ordinal != 1)
                {
                    return null;
                }

                var concatenation = (Concatenation)this.Element;
                return (PathAbsoluteOrEmpty)concatenation.Elements[2];
            }
        }

        public PathEmpty PathEmpty
        {
            get
            {
                if (this.Ordinal != 4)
                {
                    return null;
                }

                return (PathEmpty)this.Element;
            }
        }

        public PathNoScheme PathNoScheme
        {
            get
            {
                if (this.Ordinal != 3)
                {
                    return null;
                }

                return (PathNoScheme)this.Element;
            }
        }
    }
}