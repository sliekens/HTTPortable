namespace Http.Trailer
{
    public class Trailer : RequiredDelimitedList
    {
        public Trailer(RequiredDelimitedList delimitedList)
            : base(delimitedList)
        {
        }
    }
}