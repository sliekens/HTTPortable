using Txt.ABNF;

namespace Http.OWS
{
    public class OptionalWhiteSpace : Repetition
    {
        public OptionalWhiteSpace(Repetition repetition)
            : base(repetition)
        {
        }

        public override string GetWellFormedText()
        {
            var elements = Elements;
            if (elements.Count == 0)
            {
                return string.Empty;
            }

            return " ";
        }
    }
}