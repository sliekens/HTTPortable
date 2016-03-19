namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class UserInformation : Repetition
    {
        public UserInformation(Repetition repetition)
            : base(repetition)
        {
        }

        public string Password
        {
            get
            {
                var indexOfSeparator = Text.IndexOf(':');
                if (indexOfSeparator == -1)
                {
                    return null;
                }

                return Text.Substring(indexOfSeparator + 1);
            }
        }

        public string UserName
        {
            get
            {
                if (Text == string.Empty)
                {
                    return null;
                }

                var indexOfSeparator = Text.IndexOf(':');
                if (indexOfSeparator == -1)
                {
                    return Text;
                }

                return Text.Substring(0, indexOfSeparator);
            }
        }
    }
}