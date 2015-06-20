namespace Http.Grammar
{
    public class TransferEncodings : OptionalDelimitedList
    {
        public TransferEncodings(OptionalDelimitedList delimitedList)
            : base(delimitedList)
        {
        }
    }
}