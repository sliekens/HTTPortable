namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class Authority : Concatenation
    {
        public Authority(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public Host Host => (Host)this.Elements[1];

        public Port Port
        {
            get
            {
                var optionalPort = (Repetition)this.Elements[2];
                if (optionalPort.Elements.Count == 0)
                {
                    return null;
                }

                var portConcatenation = (Concatenation)optionalPort.Elements[0];
                return (Port)portConcatenation.Elements[1];
            }
        }

        public UserInformation UserInformation
        {
            get
            {
                var optionalUserInfo = (Repetition)this.Elements[0];
                if (optionalUserInfo.Elements.Count == 0)
                {
                    return null;
                }

                var userInfoConcatenation = (Concatenation)optionalUserInfo.Elements[0];
                return (UserInformation)userInfoConcatenation.Elements[0];
            }
        }
    }
}