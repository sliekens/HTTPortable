namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class PathAbsoluteOrEmpty : Repetition
    {
        public PathAbsoluteOrEmpty(Repetition sequence)
            : base(sequence)
        {
        }
    }
}