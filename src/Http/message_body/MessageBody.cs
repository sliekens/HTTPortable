using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.message_body
{
    public class MessageBody : Repetition
    {
        public MessageBody([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}