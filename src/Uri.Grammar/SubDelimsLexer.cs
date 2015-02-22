using Text.Scanning;

namespace Uri.Grammar
{
    public class SubDelimsLexer : Lexer<SubDelims>
    {
        public SubDelimsLexer()
            : base("sub-delims")
        {
        }

        public override bool TryRead(ITextScanner scanner, out SubDelims element)
        {
            if (scanner.EndOfInput)
            {
                return Default(out element);
            }

            var context = scanner.GetContext();
            foreach (var c in new[] { '!', '$', '&', '\'', '(', ')', '*', '+', ',', ';', '=' })
            {
                if (scanner.TryMatch(c))
                {
                    element = new SubDelims(c, context);
                    return true;
                }
            }

            return Default(out element);
        }
    }
}