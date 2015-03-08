using System.Diagnostics.Contracts;

using SLANG.Core;

namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using SLANG.Core;

    public class TokenCharacter : Element
    {
        public TokenCharacter(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data == '!' || data == '#' || data == '$' || data == '%' || data == '&' || data == '\'' ||
                              data == '*' || data == '+' || data == '-' || data == '.' || data == '^' || data == '_' ||
                              data == '`' || data == '|' || data == '~');
        }

        public TokenCharacter(Digit digit, ITextContext context)
            : base(digit.Data, context)
        {
            Contract.Requires(digit != null);
        }

        public TokenCharacter(Alpha alpha, ITextContext context)
            : base(alpha.Data, context)
        {
            Contract.Requires(alpha != null);
        }
    }
}