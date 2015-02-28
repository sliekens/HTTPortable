namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using Text.Scanning;

    public class Fragment : Element
    {
        public Fragment(IList<Alternative<PathCharacter, Element>> data, ITextContext context)
            : base(string.Concat(data.Select(element => element.Data)), context)
        {
            Contract.Requires(data != null);
            Contract.Requires(
                Contract.ForAll(
                    data, 
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

                            if (element is PathCharacter)
                            {
                                return true;
                            }

                            if (element.Data == "/")
                            {
                                return true;
                            }

                            if (element.Data == "?")
                            {
                                return true;
                            }

                            return false;
                        }));
        }
    }
}