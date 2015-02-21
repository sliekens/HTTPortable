using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class TokenLexer : Lexer<Token>
    {
        private readonly ILexer<TCharToken> tCharLexer;

        public TokenLexer()
            : this(new TCharLexer())
        {
        }

        public TokenLexer(ILexer<TCharToken> tCharLexer)
        {
            Contract.Requires(tCharLexer != null);
            this.tCharLexer = tCharLexer;
        }

        public override Token Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            Token token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'element'");
        }

        public override bool TryRead(ITextScanner scanner, out Token token)
        {
            if (scanner.EndOfInput)
            {
                token = default(Token);
                return false;
            }

            var context = scanner.GetContext();
            TCharToken tChar;
            if (!this.tCharLexer.TryRead(scanner, out tChar))
            {
                token = default(Token);
                return false;
            }

            List<TCharToken> list = new List<TCharToken> { tChar };
            while (this.tCharLexer.TryRead(scanner, out tChar))
            {
                list.Add(tChar);
            }

            token = new Token(new ReadOnlyCollection<TCharToken>(list), context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tCharLexer != null);
        }

    }
}
