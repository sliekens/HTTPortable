namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;

    using SLANG;
    using SLANG.Core;

    public class StatusCode : Repetition<Digit>
    {
        public StatusCode(IList<Digit> elements, ITextContext context)
            : base(elements, 3, 3, context)
        {
        }

        public int ToInt()
        {
            return int.Parse(this.Data);
        }
    }
}