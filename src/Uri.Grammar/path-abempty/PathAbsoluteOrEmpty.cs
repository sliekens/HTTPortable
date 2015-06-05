namespace Uri.Grammar
{
    using SLANG;

    public class PathAbsoluteOrEmpty : Repetition
    {
        public PathAbsoluteOrEmpty(Repetition sequence)
            : base(sequence)
        {
        }
    }
}