using System;
using Http.header_field;
using Http.message_body;
using Http.start_line;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.Core;

namespace Http.HTTP_message
{
    public sealed class HttpMessageLexerFactory : RuleLexerFactory<HttpMessage>
    {
        static HttpMessageLexerFactory()
        {
            Default = new HttpMessageLexerFactory(
                start_line.StartLineLexerFactory.Default.Singleton(),
                header_field.HeaderFieldLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.CRLF.NewLineLexerFactory.Default.Singleton(),
                message_body.MessageBodyLexerFactory.Default.Singleton());
        }

        public HttpMessageLexerFactory(
            [NotNull] ILexerFactory<StartLine> startLineLexerFactory,
            [NotNull] ILexerFactory<HeaderField> headerFieldLexerFactory,
            [NotNull] ILexerFactory<NewLine> newLineLexerFactory,
            [NotNull] ILexerFactory<MessageBody> messageBodyLexerFactory)
        {
            if (startLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(startLineLexerFactory));
            }
            if (headerFieldLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(headerFieldLexerFactory));
            }
            if (newLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(newLineLexerFactory));
            }
            if (messageBodyLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(messageBodyLexerFactory));
            }
            StartLineLexerFactory = startLineLexerFactory;
            HeaderFieldLexerFactory = headerFieldLexerFactory;
            NewLineLexerFactory = newLineLexerFactory;
            MessageBodyLexerFactory = messageBodyLexerFactory;
        }

        [NotNull]
        public static HttpMessageLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HeaderField> HeaderFieldLexerFactory { get; }

        [NotNull]
        public ILexerFactory<MessageBody> MessageBodyLexerFactory { get; }

        [NotNull]
        public ILexerFactory<NewLine> NewLineLexerFactory { get; }

        [NotNull]
        public ILexerFactory<StartLine> StartLineLexerFactory { get; }

        public override ILexer<HttpMessage> Create()
        {
            var crlf = NewLineLexerFactory.Create();
            var innerLexer = Concatenation.Create(
                StartLineLexerFactory.Create(),
                Repetition.Create(
                    Concatenation.Create(HeaderFieldLexerFactory.Create(), crlf),
                    0,
                    int.MaxValue),
                crlf,
                Option.Create(MessageBodyLexerFactory.Create()));
            return new HttpMessageLexer(innerLexer);
        }
    }
}
