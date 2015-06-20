namespace Http.Grammar
{
    using TextFx.ABNF;

    public class AsteriskForm : TerminalString
    {
        public AsteriskForm(TerminalString element)
            : base(element)
        {
        }
    }
}