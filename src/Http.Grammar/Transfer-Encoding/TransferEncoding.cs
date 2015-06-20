namespace Http.Grammar
{
    public class TransferEncoding : RequiredDelimitedList
    {
        public TransferEncoding(RequiredDelimitedList list)
            : base(list)
        {
        }
    }
}