namespace Http.Grammar
{
    using TextFx.ABNF;

    public class FieldVisibleCharacter : Alternative
    {
        public FieldVisibleCharacter(Alternative alternative)
            : base(alternative)
        {
        }
    }
}