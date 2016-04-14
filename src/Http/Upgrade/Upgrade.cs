namespace Http.Upgrade
{
    public class Upgrade : RequiredDelimitedList
    {
        public Upgrade(RequiredDelimitedList delimitedList)
            : base(delimitedList)
        {
        }
    }
}