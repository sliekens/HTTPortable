namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class Authority : Sequence
    {
        public Authority(Sequence sequence)
            : base(sequence)
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

                var portSequence = (Sequence)optionalPort.Elements[0];
                return (Port)portSequence.Elements[1];
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

                var userInfoSequence = (Sequence)optionalUserInfo.Elements[0];
                return (UserInformation)userInfoSequence.Elements[0];
            }
        }
    }
}