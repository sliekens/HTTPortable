using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.message_body
{
    public sealed class MessageBodyLexer : CompositeLexer<Repetition, MessageBody>
    {
        public MessageBodyLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
