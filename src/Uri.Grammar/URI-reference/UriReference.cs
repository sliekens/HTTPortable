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
                Debug.Assert(Ordinal == 1 || Ordinal == 2, "this.Ordinal == 1 || this.Ordinal == 2");
                if (Ordinal == 1)
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
                if (Ordinal != 2)
                {
                    return null;
                }

                return (RelativeReference)Element;
            }
        }

        public UniformResourceIdentifier UniformResourceIdentifier
        {
            get
            {
                if (Ordinal != 1)
                {
                    return null;
                }

                return (UniformResourceIdentifier)Element;
            }
        }
    }
}