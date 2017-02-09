using System;
using Http.qdtext;
using Http.quoted_pair;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.DQUOTE;
using Txt.Core;

namespace Http.quoted_string
{
    public sealed class QuotedStringLexerFactory : RuleLexerFactory<QuotedString>
    {
        static QuotedStringLexerFactory()
        {
            Default = new QuotedStringLexerFactory(
                Txt.ABNF.Core.DQUOTE.DoubleQuoteLexerFactory.Default.Singleton(),
                qdtext.QuotedTextLexerFactory.Default.Singleton(),
                quoted_pair.QuotedPairLexerFactory.Default.Singleton());
        }

        public QuotedStringLexerFactory(
            [NotNull] ILexerFactory<DoubleQuote> doubleQuoteLexerFactory,
            [NotNull] ILexerFactory<QuotedText> quotedTextLexerFactory,
            [NotNull] ILexerFactory<QuotedPair> quotedPairLexerFactory)
        {
            if (doubleQuoteLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(doubleQuoteLexerFactory));
            }
            if (quotedTextLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(quotedTextLexerFactory));
            }
            if (quotedPairLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(quotedPairLexerFactory));
            }
            DoubleQuoteLexerFactory = doubleQuoteLexerFactory;
            QuotedTextLexerFactory = quotedTextLexerFactory;
            QuotedPairLexerFactory = quotedPairLexerFactory;
        }

        [NotNull]
        public static QuotedStringLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<DoubleQuote> DoubleQuoteLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<QuotedPair> QuotedPairLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<QuotedText> QuotedTextLexerFactory { get; set; }

        public override ILexer<QuotedString> Create()
        {
            var dquote = DoubleQuoteLexerFactory.Create();
            var innerLexer = Concatenation.Create(
                dquote,
                Repetition.Create(
                    Alternation.Create(QuotedTextLexerFactory.Create(), QuotedPairLexerFactory.Create()),
                    0,
                    int.MaxValue),
                dquote);
            return new QuotedStringLexer(innerLexer);
        }
    }
}
