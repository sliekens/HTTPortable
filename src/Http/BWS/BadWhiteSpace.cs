using Http.OWS;
using JetBrains.Annotations;

namespace Http.BWS
{
    public class BadWhiteSpace : OptionalWhiteSpace
    {
        public BadWhiteSpace([NotNull] OptionalWhiteSpace optionalWhiteSpace)
            : base(optionalWhiteSpace)
        {
        }
    }
}
