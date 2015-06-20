namespace Http.Grammar
{
    public class Upgrade : RequiredDelimitedList
    {
        public Upgrade(RequiredDelimitedList delimitedList)
            : base(delimitedList)
        {
        }
    }
}