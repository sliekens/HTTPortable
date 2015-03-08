namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;



    public class DecimalOctet : Element
    {
        public DecimalOctet(Digit digit, ITextContext context)
            : base(digit.Data, context)
        {
            Contract.Requires(digit != null);
            Contract.Requires(context != null);
        }

        public DecimalOctet(char multipleOfTen, Digit digit, ITextContext context)
            : base(string.Concat(char.ToString(multipleOfTen), digit.Data), context)
        {
            Contract.Requires(multipleOfTen >= '\x0031' && multipleOfTen <= '\x0039');
            Contract.Requires(digit != null);
            Contract.Requires(context != null);
        }

        public DecimalOctet(char terminal, Digit digit1, Digit digit2, ITextContext context)
            : base(string.Concat("1", digit1.Data, digit2.Data), context)
        {
            Contract.Requires(terminal == '1');
            Contract.Requires(digit1 != null);
            Contract.Requires(digit2 != null);
            Contract.Requires(context != null);
        }

        public DecimalOctet(char terminal1, char terminal2, Digit digit, ITextContext context)
            : base(string.Concat("2", char.ToString(terminal2), digit.Data), context)
        {
            Contract.Requires(terminal1 == '2');
            Contract.Requires(terminal2 >= '\x0030' && terminal2 <= '\x0034');
            Contract.Requires(digit != null);
            Contract.Requires(context != null);
        }


        public DecimalOctet(string terminal1, char terminal2, ITextContext context)
            : base(string.Concat("25", char.ToString(terminal2)), context)
        {
            Contract.Requires(terminal1 == "25");
            Contract.Requires(terminal2 >= '\x0030' && terminal2 <= '\x0035');
            Contract.Requires(context != null);
        }
    }
}