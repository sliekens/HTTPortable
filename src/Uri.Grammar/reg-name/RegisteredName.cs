namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class RegisteredName : Repetition
    {
        public RegisteredName(Repetition repetition)
            : base(repetition)
        {
        }
    }
}