using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class TokenLexer : Lexer<Token>
    {
        private readonly ILexer<TChar> tCharLexer;

        public TokenLexer()
            : this(new TCharLexer())
        {
        }

        public TokenLexer(ILexer<TChar> tCharLexer)
            : base("token")
        {
            Contract.Requires(tCharLexer != null);
            this.tCharLexer = tCharLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Token token)
        {
            if (scanner.EndOfInput)
            {
                token = default(Token);
                return false;
            }

            var context = scanner.GetContext();
            TChar tChar;
            if (!this.tCharLexer.TryRead(scanner, out tChar))
            {
                token = default(Token);
                return false;
            }

            List<TChar> list = new List<TChar> { tChar };
            while (this.tCharLexer.TryRead(scanner, out tChar))
            {
                list.Add(tChar);
            }

            token = new Token(new ReadOnlyCollection<TChar>(list), context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tCharLexer != null);
        }

    }
}
