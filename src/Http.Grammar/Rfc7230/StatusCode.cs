using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{

    public class StatusCode : Element
    {
        public StatusCode(Digit digit1, Digit digit2, Digit digit3, ITextContext context)
            : base(string.Concat(digit1.Data, digit2.Data, digit3.Data), context)
        {
        }

        public int ToInt()
        {
            return int.Parse(this.Data);
        }
    }
}
