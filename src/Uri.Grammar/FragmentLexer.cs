namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using SLANG;

    

    public class FragmentLexer : Lexer<Fragment>
    {
        private readonly ILexer<PathCharacter> pathCharacterLexer;

        public FragmentLexer()
            : this(new PathCharacterLexer())
        {
        }

        public FragmentLexer(ILexer<PathCharacter> pathCharacterLexer)
            : base("fragment")
        {
            Contract.Requires(pathCharacterLexer != null);
            this.pathCharacterLexer = pathCharacterLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Fragment element)
        {
            var elements = new List<Alternative<PathCharacter, Element>>();
            var context = scanner.GetContext();
            while (!scanner.EndOfInput)
            {
                var innerContext = scanner.GetContext();
                PathCharacter pathCharacter;
                if (this.pathCharacterLexer.TryRead(scanner, out pathCharacter))
                {
                    elements.Add(new Alternative<PathCharacter, Element>(pathCharacter, innerContext));
                }
                else if (!scanner.EndOfInput && scanner.TryMatch('/'))
                {
                    var e = new Element('/', innerContext);
                    elements.Add(new Alternative<PathCharacter, Element>(e, innerContext));
                }
                else if (!scanner.EndOfInput && scanner.TryMatch('?'))
                {
                    var e = new Element('?', innerContext);
                    elements.Add(new Alternative<PathCharacter, Element>(e, innerContext));
                }
                else
                {
                    break;
                }
            }

            element = new Fragment(elements, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.pathCharacterLexer != null);
        }
    }
}
