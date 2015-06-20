namespace Http.Grammar
{
    using TextFx.ABNF;

    public class TransferEncodings : OptionalDelimitedList
    {
        public TransferEncodings(Repetition repetition)
            : base(repetition)
        {
        }
    }
}