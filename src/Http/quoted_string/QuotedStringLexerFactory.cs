using System;
using Http.qdtext;
using Http.quoted_pair;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.DQUOTE;
using Txt.Core;

namespace Http.quoted_string
{
    public class QuotedStringLexerFactory : ILexerFactory<QuotedString>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<DoubleQuote> doubleQuoteLexer;

        private readonly ILexer<QuotedPair> quotedPairLexer;

        private readonly ILexer<QuotedText> quotedTextLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public QuotedStringLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<DoubleQuote> doubleQuoteLexer,
            [NotNull] ILexer<QuotedText> quotedTextLexer,
            [NotNull] ILexer<QuotedPair> quotedPairLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (doubleQuoteLexer == null)
            {
                throw new ArgumentNullException(nameof(doubleQuoteLexer));
            }
            if (quotedTextLexer == null)
            {
                throw new ArgumentNullException(nameof(quotedTextLexer));
            }
            if (quotedPairLexer == null)
            {
                throw new ArgumentNullException(nameof(quotedPairLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.doubleQuoteLexer = doubleQuoteLexer;
            this.quotedTextLexer = quotedTextLexer;
            this.quotedPairLexer = quotedPairLexer;
        }

        public ILexer<QuotedString> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                doubleQuoteLexer,
                repetitionLexerFactory.Create(
                    alternationLexerFactory.Create(quotedTextLexer, quotedPairLexer),
                    0,
                    int.MaxValue),
                doubleQuoteLexer);
            return new QuotedStringLexer(innerLexer);
        }
    }
}
