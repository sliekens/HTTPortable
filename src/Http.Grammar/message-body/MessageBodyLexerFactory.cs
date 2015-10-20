namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    public class MessageBodyLexerFactory : ILexerFactory<MessageBody>
    {
        private readonly ILexerFactory<Octet> octetLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public MessageBodyLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<Octet> octetLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (octetLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(octetLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.octetLexerFactory = octetLexerFactory;
        }

        public ILexer<MessageBody> Create()
        {
            var innerLexer = this.repetitionLexerFactory.Create(this.octetLexerFactory.Create(), 1, int.MaxValue);
            return new MessageBodyLexer(innerLexer);
        }
    }
}