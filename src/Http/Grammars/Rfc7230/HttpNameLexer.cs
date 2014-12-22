﻿namespace Http.Grammars.Rfc7230
{
    using Text.Scanning;

    public class HttpNameLexer : Lexer<HttpNameToken>
    {
        public override HttpNameToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            HttpNameToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected HTTP-name");
        }

        public override bool TryRead(ITextScanner scanner, out HttpNameToken token)
        {
            var context = scanner.GetContext();

            // H
            if (!scanner.TryMatch('\u0048'))
            {
                token = default(HttpNameToken);
                return false;
            }

            // T
            if (!scanner.TryMatch('\u0054'))
            {
                scanner.PutBack('\u0048');
                token = default(HttpNameToken);
                return false;
            }

            // T
            if (!scanner.TryMatch('\u0054'))
            {
                scanner.PutBack('\u0054');
                scanner.PutBack('\u0048');
                token = default(HttpNameToken);
                return false;
            }

            // P
            if (!scanner.TryMatch('\u0050'))
            {
                scanner.PutBack('\u0054');
                scanner.PutBack('\u0054');
                scanner.PutBack('\u0048');
                token = default(HttpNameToken);
                return false;
            }

            token = new HttpNameToken(context);
            return true;
        }
    }
}