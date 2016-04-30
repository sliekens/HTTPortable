using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;

namespace Http.Content_Length
{
    public class ContentLengthLexerFactory : ILexerFactory<ContentLength>
    {
        private readonly ILexer<Digit> digitLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public ContentLengthLexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Digit> digitLexer)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (digitLexer == null)
            {
                throw new ArgumentNullException(nameof(digitLexer));
            }
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.digitLexer = digitLexer;
        }

        public ILexer<ContentLength> Create()
        {
            return new ContentLengthLexer(repetitionLexerFactory.Create(digitLexer, 1, int.MaxValue));
        }
    }
}
