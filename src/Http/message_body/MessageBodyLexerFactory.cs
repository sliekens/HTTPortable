using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.OCTET;
using Txt.Core;

namespace Http.message_body
{
    public sealed class MessageBodyLexerFactory : RuleLexerFactory<MessageBody>
    {
        static MessageBodyLexerFactory()
        {
            Default = new MessageBodyLexerFactory(Txt.ABNF.Core.OCTET.OctetLexerFactory.Default.Singleton());
        }

        public MessageBodyLexerFactory([NotNull] ILexerFactory<Octet> octetLexerFactory)
        {
            if (octetLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(octetLexerFactory));
            }
            OctetLexerFactory = octetLexerFactory;
        }

        [NotNull]
        public static MessageBodyLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Octet> OctetLexerFactory { get; set; }

        public override ILexer<MessageBody> Create()
        {
            var innerLexer = Repetition.Create(OctetLexerFactory.Create(), 0, int.MaxValue);
            return new MessageBodyLexer(innerLexer);
        }
    }
}
