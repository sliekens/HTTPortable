namespace Http.Grammar
{
    public class TransferEncoding : RequiredDelimitedList
    {
        public TransferEncoding(RequiredDelimitedList delimitedList)
            : base(delimitedList)
        {
        }
    }
}