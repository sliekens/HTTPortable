namespace Http.Grammar
{
    using TextFx.ABNF;

    public class OptionalWhiteSpace : Repetition
    {
        public OptionalWhiteSpace(Repetition repetition)
            : base(repetition)
        {
        }

        public override string GetWellFormedText()
        {
            var elements = this.Elements;
            if (elements.Count == 0)
            {
                return string.Empty;
            }

            return " ";
        }
    }
}