using System;

namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;


    using QuotedElement = SLANG.Alternative<QuotedText, QuotedPair>;

    public class QuotedStringLexer : Lexer<QuotedString>
    {
        private readonly ILexer<DoubleQuote> doubleQuoteLexer;

        private readonly ILexer<QuotedText> quotedTextLexer;

        private readonly ILexer<QuotedPair> quotedPairLexer;

        public QuotedStringLexer()
            : this(new DoubleQuoteLexer(), new QuotedTextLexer(), new QuotedPairLexer())
        {
        }

        public QuotedStringLexer(ILexer<DoubleQuote> doubleQuoteLexer, ILexer<QuotedText> quotedTextLexer, ILexer<QuotedPair> quotedPairLexer)
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
            DoubleQuote openingQuote, closingQuote;
            if (!this.doubleQuoteLexer.TryRead(scanner, out openingQuote))
            {
                element = default(QuotedString);
                return false;
            }

            var quotedElements = new List<QuotedElement>();
            QuotedElement quotedElement;
            while (this.TryReadQuotedElement(scanner ,out quotedElement))
            {
                quotedElements.Add(quotedElement);
            }

            if (this.doubleQuoteLexer.TryRead(scanner, out closingQuote))
            {
                element = QuotedString.Create(openingQuote, quotedElements, closingQuote, context);
                return true;
            }

            if (quotedElements.Count != 0)
            {
                for (int i = quotedElements.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(quotedElements[i].Data);
                }
            }

            scanner.PutBack(openingQuote.Data);
            element = default(QuotedString);
            return false;
        }

        private bool TryReadQuotedElement(ITextScanner scanner, out QuotedElement element)
        {
            if (scanner.EndOfInput)
            {
                element = default(QuotedElement);
                return false;
            }

            var context = scanner.GetContext();
            QuotedText quotedText;
            if (this.quotedTextLexer.TryRead(scanner, out quotedText))
            {
                element = new QuotedElement(quotedText, context);
                return true;
            }

            QuotedPair quotedPair;
            if (this.quotedPairLexer.TryRead(scanner, out quotedPair))
            {
                element = new QuotedElement(quotedPair, context);
                return true;
            }

            element = default(QuotedElement);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.doubleQuoteLexer != null);
            Contract.Invariant(this.quotedTextLexer != null);
            Contract.Invariant(this.quotedPairLexer != null);
        }
    }
}