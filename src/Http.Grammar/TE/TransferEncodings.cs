namespace Http.Grammar
{
    public class TransferEncodings : OptionalDelimitedList
    {
        public TransferEncodings(OptionalDelimitedList list)
            : base(list)
        {
        }
    }
}