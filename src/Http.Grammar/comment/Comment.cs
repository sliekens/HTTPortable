namespace Http.Grammar
{
    using TextFx.ABNF;

    public class Comment : Concatenation
    {
        public Comment(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}