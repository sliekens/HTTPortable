namespace Http.TE
{
    public class TransferEncodings : OptionalDelimitedList
    {
        public TransferEncodings(OptionalDelimitedList delimitedList)
            : base(delimitedList)
        {
        }
    }
}