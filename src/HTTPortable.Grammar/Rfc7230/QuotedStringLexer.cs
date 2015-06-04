namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;
    using QuotedCharacter = SLANG.Alternative<QuotedText, QuotedPair>;
    using QuotedElement = SLANG.Repetition<SLANG.Alternative<QuotedText, QuotedPair>>;

    public class QuotedStringLexer : Lexer<QuotedString>
    {
        private readonly ILexer<DoubleQuote> doubleQuoteLexer;
        private readonly ILexer<QuotedPair> quotedPairLexer;
        private readonly ILexer<QuotedText> quotedTextLexer;

        public QuotedStringLexer()
            : this(new DoubleQuoteLexer(), new QuotedTextLexer(), new QuotedPairLexer())
        {
        }

        public QuotedStringLexer(ILexer<DoubleQuote> doubleQuoteLexer, ILexer<QuotedText> quotedTextLexer,
            ILexer<QuotedPair> quotedPairLexer)
            : base("quoted-string")
        {
            Contract.Requires(doubleQuoteLexer != null);
            Contract.Requires(quotedTextLexer != null);
            Contract.Requires(quotedPairLexer != null);
            this.doubleQuoteLexer = doubleQuoteLexer;
            this.quotedTextLexer = quotedTextLexer;
            this.quotedPairLexer = quotedPairLexer;
        }

        public override bool TryRead(ITextScanner scanner, out QuotedString element)
        {
            if (scanner.EndOfInput)
            {
                element = default(QuotedString);
                return false;
            }

            var context = scanner.GetContext();
            DoubleQuote openingQuote;
            if (!this.doubleQuoteLexer.TryRead(scanner, out openingQuote))
            {
                element = default(QuotedString);
                return false;
            }

            QuotedElement quotedElement;
            if (!this.TryReadQuotedElement(scanner, out quotedElement))
            {
                scanner.PutBack(openingQuote.Data);
                element = default(QuotedString);
                return false;
            }

            DoubleQuote closingQuote;
            if (!this.doubleQuoteLexer.TryRead(scanner, out closingQuote))
            {
                scanner.PutBack(quotedElement.Data);
                scanner.PutBack(openingQuote.Data);
                element = default(QuotedString);
                return false;
            }

            element = new QuotedString(openingQuote, quotedElement, closingQuote, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.doubleQuoteLexer != null);
            Contract.Invariant(this.quotedTextLexer != null);
            Contract.Invariant(this.quotedPairLexer != null);
        }

        private bool TryReadQuotedElement(ITextScanner scanner, out QuotedElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(QuotedElement);
                return false;
            }

            var context = scanner.GetContext();
            var elements = new List<QuotedCharacter>();
            QuotedCharacter character;
            while (this.TryReadQuotedCharacter(scanner, out character))
            {
                elements.Add(character);
            }

            element = new QuotedElement(elements, context);
            return true;
        }

        private bool TryReadQuotedCharacter(ITextScanner scanner, out QuotedCharacter element)
        {
            if (scanner.EndOfInput)
            {
                element = default(QuotedCharacter);
                return false;
            }

            var context = scanner.GetContext();
            QuotedText quotedText;
            if (this.quotedTextLexer.TryRead(scanner, out quotedText))
            {
                element = new QuotedCharacter(quotedText, 1, context);
                return true;
            }

            QuotedPair quotedPair;
            if (this.quotedPairLexer.TryRead(scanner, out quotedPair))
            {
                element = new QuotedCharacter(quotedPair, 2, context);
                return true;
            }

            element = default(QuotedCharacter);
            return false;
        }
    }
}