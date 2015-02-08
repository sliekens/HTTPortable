using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class TokenLexer : Lexer<TokenToken>
    {
        private readonly ILexer<tchar> tcharLexer;

        public TokenLexer()
            : this(new tcharLexer())
        {
        }

        public TokenLexer(ILexer<tchar> tcharLexer)
        {
            Contract.Requires(tcharLexer != null);
            this.tcharLexer = tcharLexer;
        }

        public override TokenToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            TokenToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'token'");
        }

        public override bool TryRead(ITextScanner scanner, out TokenToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(TokenToken);
                return false;
            }

            var context = scanner.GetContext();
            tchar tchar;
            if (!this.tcharLexer.TryRead(scanner, out tchar))
            {
                token = default(TokenToken);
                return false;
            }

            List<tchar> list = new List<tchar> { tchar };
            while (this.tcharLexer.TryRead(scanner, out tchar))
            {
                list.Add(tchar);
            }

            token = new TokenToken(new ReadOnlyCollection<tchar>(list), context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tcharLexer != null);
        }

    }
}
