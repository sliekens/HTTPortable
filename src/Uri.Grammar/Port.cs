namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;



    /// <summary>Represents a port number.</summary>
    public class Port : Element
    {
        /// <summary>Initializes a new instance of the <see cref="Port"/> class.</summary>
        /// <param name="digits">The digits in the port number.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Port(IList<Digit> digits, ITextContext context)
            : base(string.Concat(digits), context)
        {
            Contract.Requires(digits != null);
            Contract.Requires(Contract.ForAll(digits, digit => digit != null));
        }

        public int ToInt()
        {
            int result;
            if (int.TryParse(this.Data, out result))
            {
                return result;
            }

            return 0;
        }
    }
}
