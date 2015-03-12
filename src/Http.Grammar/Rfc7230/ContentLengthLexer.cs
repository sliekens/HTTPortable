namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class ContentLengthLexer : Lexer<ContentLength>
    {
        private readonly ILexer<Digit> digitLexer;

        public ContentLengthLexer(ILexer<Digit> digitLexer)
            : base("Content-Length")
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
        }

        public override bool TryRead(ITextScanner scanner, out ContentLength element)
        {
            if (scanner.EndOfInput)
            {
                element = default(ContentLength);
                return false;
            }

            var context = scanner.GetContext();
            var elements = new List<Digit>();
            Digit digit;
            while (this.digitLexer.TryRead(scanner, out digit))
            {
                elements.Add(digit);
            }

            if (elements.Count == 0)
            {
                element = default(ContentLength);
                return false;
            }

            element = new ContentLength(elements, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.digitLexer != null);
        }
    }
}