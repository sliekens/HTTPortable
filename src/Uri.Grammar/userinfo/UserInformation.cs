namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class UserInformation : Repetition
    {
        public UserInformation(Repetition sequence)
            : base(sequence)
        {
        }

        public string Password
        {
            get
            {
                var indexOfSeparator = this.Value.IndexOf(':');
                if (indexOfSeparator == -1)
                {
                    return null;
                }

                return this.Value.Substring(indexOfSeparator + 1);
            }
        }

        public string UserName
        {
            get
            {
                if (this.Value == string.Empty)
                {
                    return null;
                }

                var indexOfSeparator = this.Value.IndexOf(':');
                if (indexOfSeparator == -1)
                {
                    return this.Value;
                }

                return this.Value.Substring(0, indexOfSeparator);
            }
        }
    }
}