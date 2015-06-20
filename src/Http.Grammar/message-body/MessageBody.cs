namespace Http.Grammar
{
    using TextFx.ABNF;

    public class MessageBody : Repetition
    {
        public MessageBody(Repetition repetition)
            : base(repetition)
        {
        }
    }
}