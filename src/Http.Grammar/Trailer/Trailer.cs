namespace Http.Grammar
{
    public class Trailer : RequiredDelimitedList
    {
        public Trailer(RequiredDelimitedList list)
            : base(list)
        {
        }
    }
}