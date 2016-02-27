namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class PathAbsoluteOrEmpty : Repetition
    {
        public PathAbsoluteOrEmpty(Repetition repetition)
            : base(repetition)
        {
        }
    }
}