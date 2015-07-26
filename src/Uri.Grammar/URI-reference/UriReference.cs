namespace Uri.Grammar
{
    using System.Diagnostics;

    using TextFx.ABNF;

    public class UriReference : Alternative
    {
        public UriReference(Alternative alternative)
            : base(alternative)
        {
        }

        public bool IsAbsolute
        {
            get
            {
                Debug.Assert(this.Ordinal == 1 || this.Ordinal == 2, "this.Ordinal == 1 || this.Ordinal == 2");
                if (this.Ordinal == 1)
                {
                    return true;
                }

                return false;
            }
        }

        public RelativeReference RelativeReference
        {
            get
            {
                if (this.Ordinal != 2)
                {
                    return null;
                }

                return (RelativeReference)this.Element;
            }
        }

        public UniformResourceIdentifier UniformResourceIdentifier
        {
            get
            {
                if (this.Ordinal != 1)
                {
                    return null;
                }

                return (UniformResourceIdentifier)this.Element;
            }
        }
    }
}