using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Http.Content_Length
{
    public sealed class ContentLengthLexerFactory : RuleLexerFactory<ContentLength>
    {
        static ContentLengthLexerFactory()
        {
            Default = new ContentLengthLexerFactory(Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default.Singleton());
        }

        public ContentLengthLexerFactory([NotNull] ILexerFactory<Digit> digitLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            DigitLexerFactory = digitLexerFactory;
        }

        [NotNull]
        public static ContentLengthLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Digit> DigitLexerFactory { get; set; }

        public override ILexer<ContentLength> Create()
        {
            var innerLexer = Repetition.Create(DigitLexerFactory.Create(), 1, int.MaxValue);
            return new ContentLengthLexer(innerLexer);
        }
    }
}
