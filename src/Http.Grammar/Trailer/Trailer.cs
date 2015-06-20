namespace Http.Grammar
{
    public class Trailer : RequiredDelimitedList
    {
        public Trailer(RequiredDelimitedList delimitedList)
            : base(delimitedList)
        {
        }
    }
}