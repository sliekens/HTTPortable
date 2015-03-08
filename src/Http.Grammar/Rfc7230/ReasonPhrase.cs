using System.Collections.Generic;
using System.Diagnostics.Contracts;

using SLANG.Core;

namespace Http.Grammar.Rfc7230
{
    using System.Linq;

    using SLANG;
    using SLANG.Core;

    public class ReasonPhrase : Element
    {
        public ReasonPhrase(IList<Alternative<HorizontalTab, Space, VisibleCharacter, ObsoletedText>> elements, ITextContext context)
            : base(string.Concat(elements.Select(element => element.Data)), context)
        {
            Contract.Requires(elements != null);
        }
    }
}