using System.Collections.Generic;
using System.Linq;

namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class Segment : Element
    {
        public Segment(IList<PathCharacter> pathCharacters, ITextContext context)
            : base(string.Concat(pathCharacters.Select(character => character.Data)), context)
        {
            Contract.Requires(pathCharacters != null);
            Contract.Requires(context != null);
            Contract.Requires(Contract.ForAll(pathCharacters, character => character != null));
        }
    }
}
