using System;
using Http.header_field;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.Core;

namespace Http.trailer_part
{
    public class TrailerPartLexerFactory : ILexerFactory<TrailerPart>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<HeaderField> headerFieldLexer;

        private readonly ILexer<NewLine> newLineLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public TrailerPartLexerFactory(
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<HeaderField> headerFieldLexer,
            [NotNull] ILexer<NewLine> newLineLexer)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (headerFieldLexer == null)
            {
                throw new ArgumentNullException(nameof(headerFieldLexer));
            }
            if (newLineLexer == null)
            {
                throw new ArgumentNullException(nameof(newLineLexer));
            }
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.headerFieldLexer = headerFieldLexer;
            this.newLineLexer = newLineLexer;
        }

        public ILexer<TrailerPart> Create()
        {
            return
                new TrailerPartLexer(
                    repetitionLexerFactory.Create(
                        concatenationLexerFactory.Create(headerFieldLexer, newLineLexer),
                        0,
                        int.MaxValue));
        }
    }
}
