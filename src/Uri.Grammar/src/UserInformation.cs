using UserInfoCharacter = SLANG.Alternative
    <Uri.Grammar.Unreserved, Uri.Grammar.PercentEncoding, Uri.Grammar.SubcomponentsDelimiter, SLANG.Element>;

namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;

    public class UserInformation : Element
    {
        public UserInformation(IList<UserInfoCharacter> elements, ITextContext context)
            : base(string.Concat(elements), context)
        {
            Contract.Requires(elements != null);
            Contract.Requires(Contract.ForAll(elements, element => element != null));
        }
    }
}