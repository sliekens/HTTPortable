namespace Http.Grammar.Rfc7230
{
    using SLANG;

    

    public class AsteriskFormLexer : Lexer<AsteriskForm>
    {
        public AsteriskFormLexer()
            : base("asterisk-form")
        {
        }

        public override bool TryRead(ITextScanner scanner, out AsteriskForm element)
        {
            if (scanner.EndOfInput)
            {
                element = default(AsteriskForm);
                return false;
            }

            var context = scanner.GetContext();
            Element asterisk;
            if (TryReadTerminal(scanner, '*', out asterisk))
            {
                element = new AsteriskForm(asterisk, context);
                return true;
            }

            element = default(AsteriskForm);
            return false;
        }
    }
}