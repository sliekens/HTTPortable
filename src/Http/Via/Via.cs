namespace Http.Via
{
    public class Via : RequiredDelimitedList
    {
        public Via(RequiredDelimitedList delimitedList)
            : base(delimitedList)
        {
        }
    }
}