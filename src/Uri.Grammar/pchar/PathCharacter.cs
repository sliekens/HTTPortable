namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class PathCharacter : Alternative
    {
        public PathCharacter(Alternative element)
            : base(element)
        {
        }
    }
}