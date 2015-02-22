using Text.Scanning;

namespace Uri.Grammar
{
    public class SubDelimsLexer : Lexer<SubDelims>
    {
        public override SubDelims Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            SubDelims element;
            if (TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'sub-delims'");
        }

        public override bool TryRead(ITextScanner scanner, out SubDelims element)
        {
            if (scanner.EndOfInput)
            {
                return Default(out element);
            }

            var context = scanner.GetContext();
            foreach (var c in new[] {'!', '$', '&', '\'', '(', ')', '*', '+', ',', ';', '='})
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