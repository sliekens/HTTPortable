using System.Collections.Generic;
using System.Linq;

namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class SegmentNonZero : Element
    {
        public SegmentNonZero(IList<PathCharacter> pathCharacters, ITextContext context)
            : base(string.Concat(pathCharacters.Select(character => character.Data)), context)
        {
            Contract.Requires(pathCharacters != null);
            Contract.Requires(pathCharacters.Count != 0);
            Contract.Requires(Contract.ForAll(pathCharacters, character => character != null));
            Contract.Requires(context != null);
        }
    }
}
