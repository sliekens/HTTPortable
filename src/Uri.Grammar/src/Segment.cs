namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using SLANG;

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