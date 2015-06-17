namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class UserInformation : Repetition
    {
        public UserInformation(Repetition sequence)
            : base(sequence)
        {
        }
    }
}