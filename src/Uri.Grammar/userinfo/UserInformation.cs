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
                var indexOfSeparator = this.Text.IndexOf(':');
                if (indexOfSeparator == -1)
                {
                    return null;
                }

                return this.Text.Substring(indexOfSeparator + 1);
            }
        }

        public string UserName
        {
            get
            {
                if (this.Text == string.Empty)
                {
                    return null;
                }

                var indexOfSeparator = this.Text.IndexOf(':');
                if (indexOfSeparator == -1)
                {
                    return this.Text;
                }

                return this.Text.Substring(0, indexOfSeparator);
            }
        }
    }
}