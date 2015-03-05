using UserInfoCharacter = Text.Scanning.Alternative<Uri.Grammar.Unreserved, Uri.Grammar.PercentEncoding, Uri.Grammar.SubcomponentsDelimiter, Text.Scanning.Element>;

namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Text.Scanning;

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
