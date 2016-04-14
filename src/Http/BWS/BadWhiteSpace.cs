using Http.OWS;

namespace Http.BWS
{
    public class BadWhiteSpace : OptionalWhiteSpace
    {
        public BadWhiteSpace(OptionalWhiteSpace optionalWhiteSpace)
            : base(optionalWhiteSpace)
        {
        }
    }
}