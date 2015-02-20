using Text.Scanning;

namespace Uri.Grammar
{
    public class SubDelimsLexer : Lexer<SubDelimsToken>
    {
        public override SubDelimsToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            SubDelimsToken token;
            if (TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'sub-delims'");
        }

        public override bool TryRead(ITextScanner scanner, out SubDelimsToken token)
        {
            if (scanner.EndOfInput)
            {
                return Default(out token);
            }

            var context = scanner.GetContext();
            foreach (var c in new[] {'!', '$', '&', '\'', '(', ')', '*', '+', ',', ';', '='})
            {
                if (scanner.TryMatch(c))
                {
                    token = new SubDelimsToken(c, context);
                    return true;
                }
            }

            return Default(out token);
        }
    }
}