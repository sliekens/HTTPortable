using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.OCTET;
using Txt.Core;

namespace Http.message_body
{
    public class MessageBodyLexerFactory : ILexerFactory<MessageBody>
    {
        private readonly ILexer<Octet> octetLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public MessageBodyLexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Octet> octetLexer)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (octetLexer == null)
            {
                throw new ArgumentNullException(nameof(octetLexer));
            }
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.octetLexer = octetLexer;
        }

        public ILexer<MessageBody> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(octetLexer, 1, int.MaxValue);
            return new MessageBodyLexer(innerLexer);
        }
    }
}
