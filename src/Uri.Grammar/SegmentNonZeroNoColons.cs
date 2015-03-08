using System.Collections.Generic;
using System.Linq;

namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using SLANG;

    public class SegmentNonZeroNoColons : Element
    {
        public SegmentNonZeroNoColons(IList<Alternative<Unreserved, PercentEncoding, SubcomponentsDelimiter, Element>> pathCharacters, ITextContext context)
            : base(string.Concat(pathCharacters.Select(character => character.Data)), context)
        {
            Contract.Requires(pathCharacters != null);
            Contract.Requires(pathCharacters.Count != 0);
            Contract.Requires(
                Contract.ForAll(pathCharacters,
                alternative =>
                {
                    if (alternative == null)
                    {
                        return false;
                    }

                    var element = alternative.Element;
                    if (element == null)
                    {
                        return false;
                    }


                    if (element is Unreserved)
                    {
                        return true;
                    }

                    if (element is PercentEncoding)
                    {
                        return true;
                    }

                    if (element is SubcomponentsDelimiter)
                    {
                        return true;
                    }

                    if (element.Data == "@")
                    {
                        return true;
                    }

                    return false;
                }));
            Contract.Requires(context != null);
        }
    }
}
