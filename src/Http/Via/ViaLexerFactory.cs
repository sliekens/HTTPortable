using System;
using Http.received_protocol;
using JetBrains.Annotations;
using Txt;

namespace Http.Via
{
    public class ViaLexerFactory : ILexerFactory<Via>
    {
        private readonly ILexer<ReceivedProtocol> receivedProtocoLexer;

        private readonly IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory;

        public ViaLexerFactory(
            [NotNull] IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory,
            [NotNull] ILexer<ReceivedProtocol> receivedProtocoLexer)
        {
            if (requiredDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requiredDelimitedListLexerFactory));
            }
            if (receivedProtocoLexer == null)
            {
                throw new ArgumentNullException(nameof(receivedProtocoLexer));
            }
            this.requiredDelimitedListLexerFactory = requiredDelimitedListLexerFactory;
            this.receivedProtocoLexer = receivedProtocoLexer;
        }

        public ILexer<Via> Create()
        {
            var innerLexer = requiredDelimitedListLexerFactory.Create(receivedProtocoLexer);
            return new ViaLexer(innerLexer);
        }
    }
}
