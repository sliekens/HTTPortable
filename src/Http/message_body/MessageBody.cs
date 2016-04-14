using Txt.ABNF;

namespace Http.message_body
{
    public class MessageBody : Repetition
    {
        public MessageBody(Repetition repetition)
            : base(repetition)
        {
        }
    }
}