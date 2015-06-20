namespace Http.Grammar
{
    public class Upgrade : RequiredDelimitedList
    {
        public Upgrade(RequiredDelimitedList list)
            : base(list)
        {
        }
    }
}