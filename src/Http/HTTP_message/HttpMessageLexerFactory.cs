using System;
using Http.header_field;
using Http.message_body;
using Http.start_line;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.Core;

namespace Http.HTTP_message
{
    public class HttpMessageLexerFactory : ILexerFactory<HttpMessage>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<HeaderField> headerFieldLexer;

        private readonly ILexer<MessageBody> messageBodyLexer;

        private readonly ILexer<NewLine> newLineLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<StartLine> startLineLexer;

        public HttpMessageLexerFactory(
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<StartLine> startLineLexer,
            [NotNull] ILexer<HeaderField> headerFieldLexer,
            [NotNull] ILexer<NewLine> newLineLexer,
            [NotNull] ILexer<MessageBody> messageBodyLexer)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (startLineLexer == null)
            {
                throw new ArgumentNullException(nameof(startLineLexer));
            }
            if (headerFieldLexer == null)
            {
                throw new ArgumentNullException(nameof(headerFieldLexer));
            }
            if (newLineLexer == null)
            {
                throw new ArgumentNullException(nameof(newLineLexer));
            }
            if (messageBodyLexer == null)
            {
                throw new ArgumentNullException(nameof(messageBodyLexer));
            }
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.startLineLexer = startLineLexer;
            this.headerFieldLexer = headerFieldLexer;
            this.newLineLexer = newLineLexer;
            this.messageBodyLexer = messageBodyLexer;
        }

        public ILexer<HttpMessage> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                startLineLexer,
                repetitionLexerFactory.Create(
                    concatenationLexerFactory.Create(headerFieldLexer, newLineLexer),
                    0,
                    int.MaxValue),
                newLineLexer,
                optionLexerFactory.Create(messageBodyLexer));
            return new HttpMessageLexer(innerLexer);
        }
    }
}
