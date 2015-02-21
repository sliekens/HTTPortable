using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class ReasonPhrase : Element
    {
        public ReasonPhrase(IList<Element> elements, ITextContext context)
            : base(string.Concat(elements), context)
        {
            Contract.Requires(elements != null);
            Contract.Requires(Contract.ForAll(elements, token => token is HorizontalTab || token is Space || token is VisibleCharacter || token is ObsText));
        }
    }
}